using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class NaturezaParcelas
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NaturezaId { get; set; }

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Parcela { get; set; }

        public decimal Tarifacao { get; set; }

        public int DiasVencimento { get; set; }

        public bool Ativa { get; set; }
    }
}
