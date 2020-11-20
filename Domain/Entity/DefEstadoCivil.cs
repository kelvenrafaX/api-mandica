using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class DefEstadoCivil
    {
        [Key]
        public int Id { get; set; }

        public char EstadoCivil  { get; set; }

        //[MaxLength(45)]
        public string Descricao { get; set; }

    }
}
