using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Fretes
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string IdIbge { get; set; }

        public string MunicipioId { get; set; }

        public string MunicipioNome { get; set; }

        public decimal Valor { get; set; }
    }
}
