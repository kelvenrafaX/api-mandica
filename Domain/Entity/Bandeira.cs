using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Bandeira
    {
        [Key]
        public int Id { get; set; }

        public string Descricao { get; set; }

        public bool Ativa { get; set; }

        public DateTime? DataCadastro { get; set; }
    }
}
