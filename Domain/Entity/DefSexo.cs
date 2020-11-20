using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class DefSexo
    {
        [Key]
        public int Id { get; set; }

        public string Sexo  { get; set; }

        public string Descricao { get; set; }

    }
}
