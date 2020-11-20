using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
    public class Login
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }

    }

    public enum TipoUser {
        Master = 1,
        Administrador = 2,
        Comum = 3
    }
}
