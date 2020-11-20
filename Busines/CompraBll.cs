using Domain.Entity;
using Domain.Function;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class CompraBll
    {
        Context context;
        CompraRepository rep;
        EstoqueRepository repEstoque;

        public CompraBll()
        {
            context = new Context();
            rep = new CompraRepository(context);
            repEstoque = new EstoqueRepository(context);
        }

        public Paginador<Compra> GetAll()
        {
            Paginador<Compra> paginador = new Paginador<Compra>
            {
                Dados = rep.GetAll()
            };
            return paginador;
        }

        public void Add(Compra compra)
        {
            if (context.Compra.Count() > 0)
            {
                compra.Id = context.Compra.Max(x => x.Id) + 1;
            }
            else
            {
                compra.Id = 1;
            }

            if (compra.DataCompra == null)
            {
                compra.DataCompra = DateTime.Now;
            }

            foreach (var item in compra.CompraProduto)
            {
                List<Estoque> est = new List<Estoque>();
                est = repEstoque.GetAll(x => x.ProdutoId == item.ProdutoId);

                if (est.Count != 0)
                {
                    est[0].Entrada += item.Quantidade;
                    repEstoque.Update(est[0]);
                }
                else
                {
                    Estoque estoque = new Estoque();

                    if (context.Estoque.Count() == 0) { estoque.Id = 1; }
                    else { estoque.Id = context.Estoque.Max(x => x.Id) + 1; }

                    estoque.ProdutoId = item.ProdutoId;
                    estoque.Entrada = item.Quantidade;
                    estoque.Saida = 0;
                    repEstoque.Add(estoque);
                }
            }

            rep.Add(compra);
        }
    }
}
