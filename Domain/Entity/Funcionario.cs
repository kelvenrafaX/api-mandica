using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public int CargoId { get; set; }
        // Relacionamentos
        public virtual Pessoa Pessoa { get; set; }

        public virtual Cargo Cargo { get; set; }

        public virtual ICollection<UsuarioAcesso> UsuarioAcesso {get;set;}
    }
}
