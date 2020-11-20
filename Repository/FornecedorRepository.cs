using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public  class FornecedorRepository:BaseRepository<Fornecedor>       
    {
        public FornecedorRepository(Context context)
            : base(context)
        {

        }

        public int getMaxId()
        {
            if (GetAll().Count() > 0)
            {
                return _context.Set<Fornecedor>().Max(x => x.Id) + 1;
            }
            else
            {
                return 1;
            }

        }

        public int getMaxIdPessoa()
        {
            if (GetAll().Count() > 0)
            {
                return _context.Set<Pessoa>().Max(x => x.Id) + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}
