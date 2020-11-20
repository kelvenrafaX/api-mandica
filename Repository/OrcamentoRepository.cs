using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public  class OrcamentoRepository:BaseRepository<Orcamento>       
    {
        public OrcamentoRepository(Context context)
            : base(context)
        {

        }
    
    }
}
