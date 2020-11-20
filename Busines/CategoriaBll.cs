using Domain.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Busines
{
    public class CategoriaBll
    {
        readonly Context context;
        CategoriaRepository rep;

        public CategoriaBll()
        {
            context = new Context();
            rep = new CategoriaRepository(context);
        }

        public Categoria Get(int id)
        {
            Categoria categoria = new Categoria();

            categoria = rep.GetById(id);
            List<Categoria> categoriasGet = rep.GetAll();
            categoria = PreencheCategoriasFilhas(categoria, categoriasGet);

            return categoria;            
        }

        private Categoria PreencheCategoriasFilhas(Categoria categoria, List<Categoria> categoriasGet)
        {
            categoria.categoriasFilhas = new List<Categoria>();
            foreach (Categoria item in categoriasGet.Where(x => x.CategoriaPaiId == categoria.Id))
            {
                categoria.categoriasFilhas.Add(PreencheCategoriasFilhas(item, categoriasGet));
            }

            return categoria;
        } 

        public List<Categoria> Get()
        {
            List<Categoria> categorias = new List<Categoria>();
            List<Categoria> categoriasGet = rep.GetAll();
            foreach (var item in categoriasGet.Where(x => x.CategoriaPaiId == 0))
            {
                categorias.Add(PreencheCategoriasFilhas(item, categoriasGet));
            }
            return categorias;
        }

        public List<Categoria> GetByType(string type)
        {
            List<Categoria> categorias = new List<Categoria>();
            Categoria categoria = rep.GetAll(x => x.Descricao == type).First();

            GetFilhas(categoria, ref categorias);

            return categorias;
        }

        public void GetFilhas(Categoria categoriaPai, ref List<Categoria> categorias)
        {
            foreach (var item in rep.GetAll(x => x.CategoriaPaiId == categoriaPai.Id))
            {
                item.Descricao = item.Descricao;
                categorias.Add(item);
                GetFilhas(item, ref categorias);
            }
        }

        public Categoria Add(Categoria categoria)
        {
            categoria.Descricao = categoria.Descricao.ToUpper();
            categoria.Ativa = true;

            Validar(categoria);

            categoria.Id = rep.GetMaxId(x => x.Id);
            rep.Add(categoria);

            return categoria;
        }

        public void Delete(int id)
        {
            DbContextTransaction transaction = context.Database.BeginTransaction();

            try
            {
                Categoria categoria = Get(id);

                RemoveFilhas(categoria);

                transaction.Commit();
            } catch(Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

        }

        public void RemoveFilhas(Categoria categoria)
        {
            foreach (var item in categoria.categoriasFilhas)
            {
                RemoveFilhas(item);
            }

            if(new Context().Produto.Where(x => x.CategoriaId == categoria.Id).Count() > 0)
            {
                throw new Exception($"Atenção. Existem produtos com a categoria {categoria.Descricao}, atualize o produto antes de prosseguir.");
            } else
            {
                rep.Remove(categoria.Id);
            }
        }

        public void Update(Categoria categoria)
        {
            Validar(categoria);

            Categoria categoriaGet = rep.GetById(categoria.Id);
            categoriaGet.Descricao = categoria.Descricao.ToUpper();
            // categoriaGet.categoriasFilhas = null;
            rep.Update(categoriaGet);
        }

        private void Validar(Categoria categoria)
        {
            if (categoria.Descricao.Trim().Length == 0)
            {
                throw new Exception("O nome da categoria é obrigatório.");
            }

            if (rep.GetAll(x => x.Descricao.Trim().ToUpper() == categoria.Descricao.Trim().ToUpper() && x.CategoriaPaiId == categoria.CategoriaPaiId).Count() > 0)
            {
                throw new Exception("Já existe uma categoria com esse nome.");
            }
        }
    }
}
