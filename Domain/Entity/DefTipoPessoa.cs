using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class DefTipoPessoa
    {
        [Key]
        public int Id { get; set; }

        public string Tipo  { get; set; }
           
        public string Descricao { get; set; }
    }
}
