using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Estoque
    {
        [Key]
        public int Id { get; set; }

        // public virtual Loja loja { get; set; }

        public int ProdutoId { get; set; }

        public int Entrada { get; set; }

        public int Saida { get; set; }

        [NotMapped]
        public int Quantidade
        {
            get
            {
                return Entrada - Saida;
            }
        }

        // Relacionamentos
        public virtual Produto Produto { get; set; }
    }
}
