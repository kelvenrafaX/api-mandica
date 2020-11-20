using System;
using Domain.Entity;
using Domain.Helpers;
using Repository;

namespace Busines
{
    public class UsuarioBll
    {
        Context context;
        UsuarioRepository repository;

        public UsuarioBll()
        {
            this.context = new Context();
            this.repository = new UsuarioRepository(this.context);
        }

        public UsuarioAcesso get(int id) {
           
            return new UsuarioRepository(context).GetById(id);
        }

        public Funcionario ValidarAcesso(Login login)
        {
            return repository.ValidarAcesso(login);
        }
    }


}
