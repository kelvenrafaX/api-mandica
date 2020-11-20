using Domain.Entity;

namespace Repository
{
    public class PessoaRepository : BaseRepository<Pessoa>
    {

        public PessoaRepository(Context context)
            : base(context)
        {
            _context = context;
        }

        public void Inativar(int id, int status)
        {
            _context.Database.ExecuteSqlCommand($"UPDATE Pessoa SET Inativo = {status} WHERE Id = {id}");
        }
       
    }
}
