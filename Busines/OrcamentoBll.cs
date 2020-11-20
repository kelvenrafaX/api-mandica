using Domain.Entity;
using Domain.Filtros;
using Domain.Function;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Busines
{
    public class OrcamentoBll
    {
        Context context;
        OrcamentoRepository rep;
        VendaRepository repVenda;
        EstoqueRepository repEstoque;
        EntregaRepository repEntrega;

        public OrcamentoBll()
        {
            context = new Context();
            rep = new OrcamentoRepository(context);
            repVenda = new VendaRepository(context);
            repEstoque = new EstoqueRepository(context);
            repEntrega = new EntregaRepository(context);
        }

        public Paginador<Orcamento> Get(FiltroOrcamento filtro)
        {
            IQueryable<Orcamento> query = context.Orcamento;

            if (filtro.Id != 0)
            {
                query = query.Where(x => x.Id == filtro.Id);
            }

            if (filtro.TipoPedido != null && filtro.TipoPedido != "")
            {
                query = query.Where(x => x.TipoPedido.Contains(filtro.TipoPedido));
            }

            int registros = query.Count();
            List<Orcamento> orcamentos = query.ToList();
            Paginador<Orcamento> dados = new Paginador<Orcamento>
            {
                Pagina = filtro.Pagina,
                QtdeItensTotal = registros,
                ItensPorPagina = filtro.ItensPorPagina,
                QtdedePaginas = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(registros) / filtro.ItensPorPagina)),
                Dados = orcamentos
            };

            return dados;
        }

        public Orcamento Get(int id) {

            return rep.GetById(id);
        }

        public List<Orcamento> Get()
        {
            return rep.GetAll();
        }

        public void AtualizaStatus(Orcamento orcamento)
        {
            Orcamento orc = rep.GetById(orcamento.Id);

            if (orc.Status == "Aguardando confirmação")
            {
                orc.TipoPedido = "Pedido";
                orc.DataPedido = DateTime.Now;
                AtualizandoEstoque(orc);
            }
            else if (orc.Status == "Aguardando entrega")
            {
                orc.DataEntregue = DateTime.Now;
                rep.Update(orc);
            } 
            else if (orc.Status == "Aguardando recebimento")
            {
                orc.DataDevolvido = DateTime.Now;
                DevolucaoEstoque(orc);
                rep.Update(orc);
            }
        }

        public int GetIdLastOrder()
        {
            return rep.GetMaxId(x => x.Id);
        }

        public void DevolucaoEstoque(Orcamento orcamento)
        {
            foreach (var item in orcamento.OrcamentoProduto)
            {
                Produto prod = new ProdutoBll().Get(item.ProdutoId);

                // Verificando o Tipo do produto para devolução.
                // Só é dado entrada no estoque caso for um Acervo.
                if (!prod.Terceiros && prod.Tipo == Tipo.ACERVO)
                {
                    List<Estoque> estoque = new List<Estoque>();
                    estoque = repEstoque.GetAll(x => x.ProdutoId == item.ProdutoId);
                    if (estoque.Count == 0)
                    {
                        // Fazendo verificação se não existe, mas o normal é existir estoque.
                        throw new Exception("Produto sem estoque para ser devolvido!");
                    }
                    else
                    {
                        estoque[0].Saida -= item.Quantidade;
                        repEstoque.Update(estoque[0]);
                    }
                }
            }
        }

        public void AtualizandoEstoque(Orcamento orcamento)
        {
            foreach (var item in orcamento.OrcamentoProduto)
            {
                if (!new ProdutoBll().Get(item.ProdutoId).Terceiros)
                {
                    List<Estoque> estoque = new List<Estoque>();
                    estoque = repEstoque.GetAll(x => x.ProdutoId == item.ProdutoId);
                    if (estoque.Count == 0)
                    {
                        Estoque est = new Estoque
                        {
                            Id = repEstoque.GetMaxId(x => x.Id),
                            ProdutoId = item.ProdutoId,
                            Saida = item.Quantidade
                        };
                        repEstoque.Add(est);
                    }
                    else
                    {
                        estoque[0].Saida += item.Quantidade;
                        repEstoque.Update(estoque[0]);
                    }
                }
            }
        }

        public void Add(Orcamento orcamento)
        {
            // Iniciando Transação
            var connection = ((IObjectContextAdapter)context).ObjectContext.Connection;
            connection.Open();
            using (System.Data.Common.DbTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    // Ajustando timezone das datas
                    orcamento.AjusteDatas();

                    // Verificando se é um pedido com orçamento importado
                    bool orcamentoImportado = orcamento.Id != 0 && orcamento.TipoPedido == "Pedido";
                    if (!orcamentoImportado)
                        orcamento.Id = rep.GetMaxId(x => x.Id);

                    List<Venda> vendas = new List<Venda>();

                    if (orcamento.TipoPedido == "Pedido")
                    {
                        int vendaId = orcamento.Id;
                        int vendaGrupo = 0;
                        
                        foreach (var item in orcamento.Venda)
                        {
                            for (int i = 1; i <= item.Plano; i++)
                            {
                                Venda vendaItem = new Venda
                                {
                                    Id = vendaId,
                                    Grupo = vendaGrupo,
                                    Parcela = i,
                                    ValorPago = 0,
                                    ValorParcela = item.ValorParcela
                                };
                                vendaItem.DataRecebimento = DateTime.Now.AddDays(item.Natureza.NaturezaParcelas.Where(x => x.Parcela == vendaItem.Parcela).First().DiasVencimento);
                                vendaItem.DataPagamento = DateTime.Now;
                                vendaItem.NaturezaId = item.NaturezaId;
                                vendaItem.Natureza = null;
                                vendaItem.Plano = item.Plano;
                                vendaItem.ValorPago = vendaItem.ValorParcela;
                                vendas.Add(vendaItem);
                            }

                            vendaGrupo++;
                        }

                        orcamento.Venda = vendas;

                        orcamento.Entrega.Id = repEntrega.GetMaxId(x => x.Id);

                        orcamento.DataPedido = DateTime.Now;
                        AtualizandoEstoque(orcamento);
                        ValidarOrcamento(orcamento);
                    }
                    else
                    {
                        orcamento.Entrega = null;
                        ValidarOrcamento(orcamento);
                        // orcamento.DataEvento = DateTime.Now;
                    }

                    orcamento.DataDevolvido = null;
                    orcamento.Cliente = null;
                    orcamento.TipoEntrega = null;
                    orcamento.DataCadastro = DateTime.Now;

                    foreach (var item in orcamento.OrcamentoProduto)
                    {
                        item.Id = orcamento.Id;
                    }

                    if (orcamentoImportado)
                    {
                        repVenda.AddRange(orcamento.Venda);
                        rep.Update(orcamento);
                    }
                    else
                    {
                        rep.Add(orcamento);
                    }

                    transaction.Commit();
                } catch(Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Update(Orcamento orcamento)
        {
            try
            {
                Orcamento orc = rep.GetById(orcamento.Id);

                int vendaId = orc.Id;
                int vendaGrupo = orc.Venda.Max(x => x.Grupo) + 1;
                List<Venda> vendas = new List<Venda>();

                foreach (var item in orcamento.Venda)
                {
                    for (int i = 1; i <= item.Plano; i++)
                    {
                        Venda vendaItem = new Venda
                        {
                            Id = vendaId,
                            Grupo = vendaGrupo,
                            Parcela = i,
                            ValorPago = 0,
                            ValorParcela = item.ValorParcela
                        };
                        vendaItem.DataRecebimento = DateTime.Now.AddDays(item.Natureza.NaturezaParcelas.Where(x => x.Parcela == vendaItem.Parcela).First().DiasVencimento);
                        vendaItem.DataPagamento = DateTime.Now;
                        vendaItem.NaturezaId = item.NaturezaId;
                        vendaItem.Natureza = null;
                        vendaItem.Plano = item.Plano;
                        vendaItem.ValorPago = vendaItem.ValorParcela;
                        vendas.Add(vendaItem);
                        orc.Venda.Add(vendaItem);
                    }

                    vendaGrupo++;
                }

                repVenda.AddRange(vendas);
                // new OrcamentoRepository(new Context()).Update(orc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ValidarOrcamento(Orcamento orcamento)
        {
            if (orcamento.DataEvento == null)
            {
                throw new Exception("Data do Evento é obrigatório!");
            }
            else if (orcamento.Diarias <= 0)
            {
                throw new Exception("Diárias precisam ser maiores que 0!");
            }
            else if (orcamento.TipoPedido != "Orçamento" && orcamento.TipoPedido != "Pedido")
            {
                throw new Exception("Tipo de Pedido incorreto!");
            }

            if (orcamento.TipoPedido == "Pedido")
            {
                if (orcamento.DataEntrega == null)
                {
                    throw new Exception("Data de Entrega é obrigatório!");
                }
                else if (orcamento.DataDevolucao == null)
                {
                    throw new Exception("Data de Devolução é obrigatório!");
                }
                else if (orcamento.Entrega.Cep.Length <= 8 || orcamento.Entrega.Cep == null)
                {
                    throw new Exception("Cep incorreto!");
                }
                else if (orcamento.Entrega.Rua == null || orcamento.Entrega.Rua.Equals(""))
                {
                    throw new Exception("Rua é obrigatório!");
                }
                else if (orcamento.Entrega.Bairro == null || orcamento.Entrega.Bairro.Equals(""))
                {
                    throw new Exception("Bairro é obrigatório!");
                }
                else if (orcamento.Entrega.Numero == null || orcamento.Entrega.Numero.Equals(""))
                {
                    throw new Exception("Numero é obrigatório!");
                }
                else if (orcamento.Diarias > (orcamento.DataDevolucao - orcamento.DataEvento).TotalDays)
                {
                    throw new Exception("Diárias precisa ser menor ou igual a diferença de dias entre a data do evento e a data de devolução!");
                }
            }

            ValidaOrcamentoProduto(orcamento.OrcamentoProduto);
        }
        
        public void ValidaOrcamentoProduto(ICollection<OrcamentoProduto> orcamentoProduto)
        {
            if(orcamentoProduto.Count > 0)
            {
                foreach (var item in orcamentoProduto)
                {
                    if (item.Quantidade <= 0)
                    {
                        throw new Exception("Quantidade do Produto precisa ser maior que zero!");
                    }
                }
            }
        }
    }

   
}
