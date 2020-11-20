using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class ImagemProduto
    {
        [Key]
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int ImagemId { get; set; }

        // Relacionamentos
        public virtual Imagem Imagem { get; set; }
    }

}
