using Domain.Entity;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Busines
{
    public class ImagemBll
    {
        
       ImagemRepository rep = new ImagemRepository(new Context());

        public Imagem Get(int id) {
        
            return rep.GetById(id);
        }

        public List<Imagem> Get()
        {
              return rep.GetAll();
        }

       
        public void Add(Imagem imagem)
        {
            imagem.Id = rep.GetMaxId();
            rep.Add(imagem);  
        }

        public string Delete(int id)
        {
            try
            {
              rep.Remove(id);
                return "Imagem removida com sucesso!";
            }
            catch
            {
                return "Falha ao remover Imagem!";
            }
            
        }

        public int getMaxId()
        {
            return rep.GetMaxId();
        }
        public int GetLastId() {
            return rep.GetLastId();
        }

        public void AddOrUpdade(Imagem imagemProduto)
        {
            if (Get().Where(x => x.Descricao == imagemProduto.Descricao).Count() > 0)
                rep.Update(imagemProduto);
            else
                rep.Add(imagemProduto);
        }
    }


}
