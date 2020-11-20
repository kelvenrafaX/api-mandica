using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    public class Cargo
    {
        [Key]
        public int Id { get; set; }

        public string Descricao { get; set; }

        public bool Ativa { get; set; }

    }
}
