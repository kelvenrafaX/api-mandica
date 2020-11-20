using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }

        // Relacionamentos
        public virtual Pessoa Pessoa { get; set; }

    }
}
