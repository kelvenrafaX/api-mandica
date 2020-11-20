using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Loja
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }
    }
}
