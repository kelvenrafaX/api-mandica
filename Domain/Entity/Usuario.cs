using System.Collections.Generic;

namespace Domain.Entity
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public int FuncionarioId{ get; set; }

        public virtual ICollection<UsuarioAcesso> UsuarioAcessos { get; set; }


    }
}