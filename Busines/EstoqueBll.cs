using Busines;
using Domain.Entity;
using Domain.Filtros;
using Domain.Function;
using Repository;
using System;

namespace Business
{
    public class EstoqueBll
    {
        readonly Context context;
        EstoqueRepository rep;
        ProdutoBll produtoBll;

        public EstoqueBll()
        {
            context = new Context();
            rep = new EstoqueRepository(context);
            produtoBll = new ProdutoBll();
        }

        public Paginador<Estoque> GetAll()
        {
            Paginador<Estoque> paginador = new Paginador<Estoque>
            {
                Dados = rep.GetAll()
            };
            return paginador;
        }

        public Paginador<Produto> GetAll(DateTime DataInicial, DateTime DataFinal)
        {
            Paginador<Produto> paginador = new Paginador<Produto>();
            FiltroProduto filtro = new FiltroProduto();
            paginador.Dados = produtoBll.Get(Tipo.ACERVO);

            foreach (var item in paginador.Dados)
            {
                item.Quantidade = GetEstoque(item, DataInicial, DataFinal);
            }

            return paginador;
        }

        public int GetEstoque(Produto produto, DateTime DataInicialFiltro, DateTime DataFinalFiltro)
        {
            int qtd = produto.Quantidade;

            if (produto.EstoqueComprometido.Count > 0)
            {
                foreach (var x in produto.EstoqueComprometido)
                {
                    if (!((x.DataInicio < DataInicialFiltro && x.DataFim < DataInicialFiltro) || (x.DataInicio > DataFinalFiltro && x.DataFim > DataFinalFiltro)))
                    {
                        qtd -= x.Quantidade;
                    }
                }
            }

            return qtd;
        }
    }
}
