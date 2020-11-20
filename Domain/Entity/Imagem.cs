using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Imagem
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string Descricao { get; set; }

        public bool Principal { get; set; }

    }
}
