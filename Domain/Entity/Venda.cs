using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Venda
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Grupo { get; set; }

        [Key, Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Parcela { get; set; }


        public decimal ValorParcela { get; set; }

        public decimal ValorPago { get; set; }

        public DateTime? DataPagamento { get; set; }

        public DateTime? DataRecebimento { get; set; }

        public int NaturezaId { get; set; }

        public int Plano { get; set; }

        // Relacionamentos
        public virtual Natureza Natureza { get; set; }
    }
}
