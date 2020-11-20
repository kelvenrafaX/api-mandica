using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class DefTipoEntrega
    {
        [Key]
        public int TipoEntrega  { get; set; }

        public string Descricao { get; set; }

    }
}
