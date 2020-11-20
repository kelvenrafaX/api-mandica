using Domain.Entity;
using Repository;
using System;

namespace Busines
{
    public class VendaBll
    {
        readonly Context context;
        VendaRepository rep;

        public VendaBll()
        {
            context = new Context();
            rep = new VendaRepository(context);
        }

        public void PagarParcela(int id, int grupo, int parcela)
        {
            Venda venda = rep.GetAll( x => x.Id == id && x.Grupo == grupo && x.Parcela == parcela)[0];

            if (venda == null)
            {
                throw new Exception("Esse pagamento/parcela não existe!");
            }

            venda.ValorPago = venda.ValorParcela;
            // venda.DataPagamento = DateTime.Now;
            rep.Update(venda);
        }

        public void PagarTotal(Orcamento orcamento)
        {
            foreach (var item in orcamento.Venda)
            {
                Venda venda = rep.GetAll(x => x.Id == item.Id && x.Grupo == item.Grupo && x.Parcela == item.Parcela)[0];
                venda.ValorPago = venda.ValorParcela;
                // venda.DataPagamento = DateTime.Now;
                rep.Update(venda);
            }
        }
    }
}
