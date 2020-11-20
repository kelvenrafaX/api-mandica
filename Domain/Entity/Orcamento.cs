using Domain.Function;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Orcamento
    {
        [Key]
        public int Id  { get; set; }

        public int ClienteId { get; set; }

        public int TipoEntregaId { get; set; }

        public DateTime DataEntrega { get; set; }

        public DateTime? DataEntregue { get; set; }

        public DateTime DataEvento { get; set; }

        public int Diarias { get; set; }

        public string Obs { get; set; }

        public string TipoPedido { get; set; }

        public virtual ICollection<OrcamentoProduto> OrcamentoProduto { get; set; }

        public virtual ICollection<Venda> Venda { get; set; }

        public decimal Desconto { get; set; }

        public decimal Frete { get; set; }

        public DateTime? DataCadastro { get; set; }

        public DateTime? DataPedido { get; set; }

        public DateTime DataDevolucao { get; set; }

        public DateTime? DataDevolvido { get; set; }

        public decimal PercentualEntrada { get; set; }

        // Relacionamentos
        public virtual Entrega Entrega { get; set; }
        public virtual DefTipoEntrega TipoEntrega { get; set; }
        public virtual Cliente Cliente { get; set; }

        [NotMapped]
        public decimal ValorTotal
        {
            get
            {
                decimal valorTotal = 0;
                foreach (var item in OrcamentoProduto)
                {
                    valorTotal += item.ValorUnitario * item.Quantidade;
                }

                valorTotal += Frete;
                valorTotal -= Desconto;
                return valorTotal;
            }
        }

        [NotMapped]
        public string Status
        {
            get
            {
                string status;
                if (TipoPedido == "Orçamento")
                {
                    status = "Aguardando confirmação";
                }
                else if (TipoPedido == "Pedido")
                {
                    decimal valorTotalPago = 0;
                    foreach (var item in Venda)
                    {
                        valorTotalPago += item.ValorPago;
                    }

                    if (valorTotalPago < ValorTotal)
                    {
                        status = "Aguardando pagamento";
                    }
                    else if (DataEntregue == null)
                    {
                        status = "Aguardando entrega";
                    } 
                    else if(DataDevolvido == null)
                    {
                        status = "Aguardando recebimento";
                    }
                    else
                    {
                        status = "Pedido finalizado";
                    }
                }
                else
                {
                    status = "Tipo inválido!";
                }

                return status;
            }
        }

        [NotMapped]
        public double DiasRestantes
        {
            get
            {
                double dias = (DataEntrega - DateTime.Now).TotalDays;
                if (dias < 1 && dias > 0)
                {
                    return 0;
                }
                else
                {
                    return dias;
                }
            }
        }

        public void AjusteDatas()
        {
            DataEntrega = Utils.ConvertTimeBrazilian(DataEntrega);
            DataDevolucao = Utils.ConvertTimeBrazilian(DataDevolucao);
            DataEvento = Utils.ConvertTimeBrazilian(DataEvento);
        }

    }
}
