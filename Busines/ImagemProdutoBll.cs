using Domain.Entity;
using Repository;
using System.Collections.Generic;

namespace Busines
{
    public class ImagemProdutoBll
    {
        
       ImagemProdutoRepository rep = new ImagemProdutoRepository(new Context());

        public ImagemProduto Get(int id) {
        
            return rep.GetById(id);
        }

        public List<ImagemProduto> Get()
        {
              return rep.GetAll();
        }

       
        public void Add(ImagemProduto imagem)
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

        public void AddOrUpdade(ImagemProduto imagemProduto)
        {
            rep.Update(imagemProduto);
        }
    }


}
