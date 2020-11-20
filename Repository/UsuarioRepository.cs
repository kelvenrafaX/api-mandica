using Domain.Entity;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public  class UsuarioRepository:BaseRepository<UsuarioAcesso>       
    {
        public UsuarioRepository(Context context)
            : base(context)
        {

        }

        public Funcionario ValidarAcesso(Login login)
        {
            return new Funcionario();
        }
    }
}
