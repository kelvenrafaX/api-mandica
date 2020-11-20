using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public  class FretesRepository:BaseRepository<Fretes>       
    {
        private new Context _context;

        public FretesRepository(Context context)
            : base(context)
        {
            _context = context;
        }
    }
}
