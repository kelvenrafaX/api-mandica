using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class DefTipoPedido
    {
        [Key]
        public int TipoPedido  { get; set; }

        [MaxLength(45)]
        public string Descricao { get; set; }
    }
}
