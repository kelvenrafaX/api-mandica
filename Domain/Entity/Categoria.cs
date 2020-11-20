using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        public string Descricao { get; set; }

        public int CategoriaPaiId { get; set; }

        public bool Ativa { get; set; }

        [NotMapped]
        public ICollection<Categoria> categoriasFilhas { get; set; }
    }
}
