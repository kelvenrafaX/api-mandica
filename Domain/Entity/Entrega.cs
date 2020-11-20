using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Entrega
    {
        [Key]
        public int Id { get; set; }

        public string Cep { get; set; }

        public string Rua { get; set; }

        public string Bairro { get; set; }

        public string Numero { get; set; }
    }
}
