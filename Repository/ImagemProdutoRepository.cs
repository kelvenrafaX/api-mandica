using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ImagemProdutoRepository : BaseRepository<ImagemProduto>
    {
        private new Context _context;

        public ImagemProdutoRepository(Context context)
            : base(context)
        {
            _context = context;
        }

        public int GetMaxId()
        {
            if (_context.ImagemProduto.Count() > 0)
            {

                return _context.ImagemProduto.Max(x => x.Id) + 1;
            }
            else
            {
                return 1;
            }


        }

        public int GetLastId()
        {
            return _context.ImagemProduto.Max(x => x.Id);
        }



    }
}
