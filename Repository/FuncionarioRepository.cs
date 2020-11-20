using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FuncionarioRepository : BaseRepository<Funcionario>
    {
        private new Context _context;

        public FuncionarioRepository(Context context)
            : base(context)
        {
            _context = context;
        }


        public int GetMaxId()
        {
            if (_context.Funcionario.Count() > 0)
            {

                return _context.Funcionario.Max(x => x.Id) + 1;
            }
            else
            {
                return 1;
            }
        }

        public int GetMaxIdPessoa()
        {
            if (_context.Pessoa.Count() > 0)
            {

                return _context.Pessoa.Max(x => x.Id) + 1;
            }
            else
            {
                return 1;
            }
        }

    }
}
