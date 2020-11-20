using Domain.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Repository
{
    public class NaturezaRepository : BaseRepository<Natureza>
    {

        public NaturezaRepository(Context context)
            : base(context)
        {
            _context = context;
        }

    }
}
