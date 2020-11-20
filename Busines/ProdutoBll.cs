using Business;
using Domain.Entity;
using Domain.Filtros;
using Domain.Function;
using Domain.Helpers;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Busines
{
    public class ProdutoBll
    {
        Context context;
        ProdutoRepository rep;
        EstoqueRepository repEstoque;
        OrcamentoRepository repOrcamento;
        ImagemProdutoRepository repImagemProduto;
        public ProdutoBll()
        {
            context = new Context();
            rep = new ProdutoRepository(context);
            repEstoque = new EstoqueRepository(context);
            repOrcamento = new OrcamentoRepository(context);
            repImagemProduto = new ImagemProdutoRepository(context);
        }

        public Produto Get(int id)
        {
            return rep.GetById(id);
        }

        public Produto Get(int id, Tipo tipo)
        {
            Produto produto = new Produto();

            if (tipo != Tipo.TODOS)
                produto = rep.GetAll(x => x.Id == id && x.Tipo == tipo).FirstOrDefault();
            else
                produto = rep.GetAll(x => x.Id == id).FirstOrDefault();

            PreencheEstoque(ref produto);
            return produto;
        }

        public List<Produto> Get(Tipo tipo)
        {
            List<Produto> produtos = new List<Produto>();

            if (tipo != Tipo.TODOS)
                produtos = rep.GetAll(x => x.Tipo == tipo);
            else
                produtos = rep.GetAll();

            PreencheEstoque(ref produtos);
            return produtos;
        }

        public List<Produto> Get()
        {
            List<Produto> produtos = rep.GetAll();
            PreencheEstoque(ref produtos);
            return produtos;
        }

        public Paginador<Produto> GetAll(FiltroProduto filtro, Tipo tipo)
        {
            IQueryable<Produto> query = context.Produto;

            if (tipo != Tipo.TODOS)
                query = query.Where(x => x.Tipo == tipo);

            if (filtro.Id > 0)
            {
                query = query.Where(x => x.Id == filtro.Id);
            }

            if (filtro.Nome != null && filtro.Nome != "")
            {
                query = query.Where(x => x.Nome.Contains(filtro.Nome));
            }

            if (filtro.Descricao != null && filtro.Descricao != "")
            {
                query = query.Where(x => x.Descricao.Contains(filtro.Descricao));
            }

            if (filtro.Fornecedor > 0)
            {
                query = query.Where(x => x.FornecedorId == filtro.Fornecedor);
            }

            if (filtro.Categoria > 0)
            {
                query = query.Where(x => x.CategoriaId == filtro.Categoria);
            }

            if (filtro.Inativo != 2)
            {
                query = query.Where(x => x.Inativo == filtro.Inativo);
            }

            if(filtro.Situacao != Situacao.TODOS)
            {
                query = query.Where(x => x.Situacao == filtro.Situacao);
            }

            if(filtro.Terceiros)
                query = query.Where(x => x.Terceiros == filtro.Terceiros);

            int registros = query.Count();
            List<Produto> produtos = query.ToList();
            Paginador<Produto> dados = new Paginador<Produto>
            {
                Pagina = filtro.Pagina,
                QtdeItensTotal = registros,
                ItensPorPagina = filtro.ItensPorPagina,
                QtdedePaginas = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(registros) / filtro.ItensPorPagina))
            };

            PreencheEstoque(ref produtos);

            dados.Dados = produtos;

            return dados;

        }

        public void PreencheEstoque(ref List<Produto> produtos)
        {
            for (int i = 0; i < produtos.Count; i++)
            {
                int id = produtos[i].Id;
                List<Estoque> estoque = new List<Estoque>();
                List<Orcamento> orcamentos = new List<Orcamento>();
                produtos[i].EstoqueComprometido = new List<EstoqueComprometido>();
                orcamentos = repOrcamento.GetAll(x => x.TipoPedido == "Pedido");
                estoque = repEstoque.GetAll(x => x.ProdutoId == id);
                if (estoque != null && estoque.Count > 0)
                {
                    produtos[i].Quantidade = estoque[0].Entrada;
                    foreach (var orc in orcamentos)
                    {
                        foreach (var orcProd in orc.OrcamentoProduto)
                        {
                            if (orcProd.ProdutoId == produtos[i].Id)
                            {
                                EstoqueComprometido estCmp = new EstoqueComprometido
                                {
                                    ProdutoId = orcProd.ProdutoId,
                                    Quantidade = orcProd.Quantidade,
                                    DataInicio = orc.DataEntrega,
                                    DataFim = orc.DataDevolucao
                                };
                                produtos[i].EstoqueComprometido.Add(estCmp);
                            }
                        }
                    }
                }
            }
        }

        public void PreencheEstoque(ref Produto produtos)
        {

            int id = produtos.Id;
            List<Estoque> estoque = new List<Estoque>();
            List<Orcamento> orcamentos = new List<Orcamento>();
            produtos.EstoqueComprometido = new List<EstoqueComprometido>();
            orcamentos = repOrcamento.GetAll(x => x.TipoPedido == "Pedido");
            estoque = repEstoque.GetAll(x => x.ProdutoId == id);
            if (estoque != null && estoque.Count > 0)
            {
                produtos.Quantidade = estoque[0].Entrada;
                foreach (var orc in orcamentos)
                {
                    foreach (var orcProd in orc.OrcamentoProduto)
                    {
                        if (orcProd.ProdutoId == produtos.Id)
                        {
                            EstoqueComprometido estCmp = new EstoqueComprometido
                            {
                                ProdutoId = orcProd.ProdutoId,
                                Quantidade = orcProd.Quantidade,
                                DataInicio = orc.DataEntrega,
                                DataFim = orc.DataDevolucao
                            };
                            produtos.EstoqueComprometido.Add(estCmp);
                        }
                    }
                }
            }
            
        }

        public Produto Update(Produto produto, int estoque = 0)
        {
            produto.Quantidade = estoque;
            Produto prod = rep.GetById(produto.Id);
            prod.FornecedorId = produto.FornecedorId;
            prod.CategoriaId = produto.CategoriaId;
            prod.Descricao = produto.Descricao;
            prod.Inativo = produto.Inativo;
            prod.Nome = produto.Nome;
            prod.ValorCustoProduto = produto.ValorCustoProduto;
            prod.ValorUnitarioLocacao = produto.ValorUnitarioLocacao;
            prod.ValorUnitarioReposicao = produto.ValorUnitarioReposicao;
            prod.Terceiros = produto.Terceiros;
            prod.Tipo = produto.Tipo;
            prod.Situacao = produto.Situacao;
            prod.EstoqueMin = produto.EstoqueMin;
            prod.EstoqueMax = produto.EstoqueMax;
            prod.Cor = produto.Cor;
            prod.Altura = produto.Altura;
            prod.Profundidade = produto.Profundidade;
            prod.Largura = produto.Largura;

            foreach (var item in produto.ImagemProdutos)
            {
                
                List<ImagemProduto> img = new List<ImagemProduto>();
                img = prod.ImagemProdutos.Where(x => x.Imagem.Id == item.Imagem.Id || x.Imagem.Descricao == item.Imagem.Descricao).ToList();
                if (img.Count() > 0)
                {
                    img.First().Imagem.Principal = item.Imagem.Principal;
                } else
                {
                    item.ProdutoId = produto.Id;
                    prod.ImagemProdutos.Add(item);
                }
            }

            repImagemProduto.RemoveRange(prod.ImagemProdutos.Where(x => produto.ImagemProdutos.Where(y => y.Imagem.Id == x.Imagem.Id || y.Imagem.Descricao == x.Imagem.Descricao).Count() == 0));

            PreencheEstoque(ref prod);

            if(estoque > 0)
            {
                if (produto.Quantidade > prod.Quantidade)
                {
                    // Realizar as Compras Automáticas necessárias
                    // Adiciona a Compra com a quantidade da diferença da Quantidade Atualizada - Quantidade Anterior
                    new CompraBll().Add(new Compra
                    {
                        DataCompra = DateTime.Now,
                        FornecedorId = prod.FornecedorId,
                        CompraProduto = new List<CompraProduto> {
                            new CompraProduto { PrecoUnitario = prod.ValorCustoProduto, ProdutoId = prod.Id, Quantidade = produto.Quantidade - prod.Quantidade }
                        },
                    });
                } else if(produto.Quantidade < prod.Quantidade)
                {
                    throw new Exception("Ao Editar, a Quantidade não pode ser menor que a atual.");
                }

                prod.Quantidade = produto.Quantidade;
            }

            rep.Update(NullDependencies(prod));

            return prod;
        }

        public void Inativar(int id, int status)
        {
            Produto produto = new Produto();
            produto = rep.GetById(id);
            produto.Inativo = status;
            rep.Update(NullDependencies(produto));
        }

        public Produto Add(Produto produto, Tipo tipo, int estoque)
        {
            if(tipo == Tipo.SERVICO)
            {
                produto.FornecedorId = context.Fornecedor.First().Id;
            }

            produto.Tipo = tipo;
            produto.Situacao = Situacao.PERFEITOESTADO;
            produto.DataCadastro = DateTime.Now;

            if(produto.Id == 0)
            {
                if (context.Produto.Count() > 0)
                {
                    produto.Id = context.Produto.Max(x => x.Id) + 1;
                }
                else
                {
                    produto.Id = 1;
                }
            }
            
            foreach (var item in produto.ImagemProdutos)
            {
                item.ProdutoId = produto.Id;
            }


            // produto.Imagem = new ImagemProduto { Id = new ImagemRepository(new Context()).getMaxId(), ImagemId = 0, ProdutoId = produto.Id };
            rep.Add(NullDependencies(produto));

            if(estoque > 0)
            {
                new CompraBll().Add(new Compra {
                    DataCompra = DateTime.Now,
                    FornecedorId = produto.FornecedorId,
                    CompraProduto = new List<CompraProduto> {
                        new CompraProduto { PrecoUnitario = produto.ValorCustoProduto, ProdutoId = produto.Id, Quantidade = estoque }
                    },
                });
            }

            return Get(produto.Id);
        }

        public int GetIdMax()
        {
            return context.Produto.Max(x => x.Id);
        }

        public Produto NullDependencies(Produto produto)
        {
            produto.Fornecedor = null;
            produto.Categoria = null;
            return produto;
        }
    }


}
