using Domain.Entity;
using Repository;
using System.Collections.Generic;

namespace Busines
{
    public class ModuloBll
    {
        ModuloRepository repository;
        Context context;
        public ModuloBll()
        {
            context = new Context();
            repository = new ModuloRepository(context);
        }

       public List<Modulos> Get()
        {

            return repository.GetAll();
        }
    }


}
