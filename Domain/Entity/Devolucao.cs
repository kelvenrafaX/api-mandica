using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Devolucao
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataDevolucao { get; set; }

        public virtual ICollection<DevolucaoProduto> DevolucaoProduto { get; set; }
    }
}
