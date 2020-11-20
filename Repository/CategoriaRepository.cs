using Domain.Entity;

namespace Repository
{
    public class CategoriaRepository : BaseRepository<Categoria>
    {

        public CategoriaRepository(Context context)
            : base(context)
        {
            _context = context;
        }
       
    }
}
