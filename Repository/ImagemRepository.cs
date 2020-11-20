using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ImagemRepository : BaseRepository<Imagem>
    {
        private new Context _context;

        public ImagemRepository(Context context)
            : base(context)
        {
            _context = context;
        }

        public int GetMaxId()
        {
            if (_context.Imagem.Count() > 0)
            {

                return _context.Imagem.Max(x => x.Id) + 1;
            }
            else
            {
                return 1;
            }


        }

        public int GetLastId()
        {
            return _context.Imagem.Max(x => x.Id);
        }



    }
}
