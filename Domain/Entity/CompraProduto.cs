namespace Domain.Entity
{
    public class CompraProduto
    {
        public int Id { get; set; }

        public int ProdutoId { get; set; }

        public decimal PrecoUnitario { get; set; }

        public int Quantidade { get; set; }

        // Relacionamentos
        public virtual Produto Produto { get; set; }
    }
}