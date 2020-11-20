using Domain.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busines
{
    public class CategoriaBll
    {
        
        CategoriaRepository rep = new CategoriaRepository(new Context());
        public Categoria Get(int id) {
        
            return rep.GetById(id);
        }

        public List<Categoria> Get()
        {
              return rep.GetAll();
        }

       
        public void Add(Categoria categoria)
        {
    
            rep.Add(categoria);  
        }

        public string Delete(int id)
        {
            try
            {
              rep.Remove(id);
                return "Categoria com id " + id + " removido com sucesso!";
            }
            catch
            {
                return "Falha ao remover cliente!";
            }
            
        }
    }


}
