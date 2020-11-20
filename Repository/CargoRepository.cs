using Domain.Entity;

namespace Repository
{
    public class CargoRepository : BaseRepository<Cargo>
    {

        public CargoRepository(Context context)
            : base(context)
        {
            _context = context;
        }
       
    }
}
