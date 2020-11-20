using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Natureza
    {
        [Key]
        public int Id { get; set; }

        public string Descricao { get; set; }

        public virtual ICollection<NaturezaParcelas> NaturezaParcelas { get; set; }

        public bool Ativa { get; set; }
    }
}
