using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class OrcamentoProduto
    {
        [Key]
        public int Id  { get; set; }

        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }

        // Relacionamentos
        public virtual Produto Produto { get; set; }
    }
}
