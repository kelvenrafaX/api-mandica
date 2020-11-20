using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace Repository
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;

        /// <summary>
        /// Construtor recebe um contexto
        /// </summary>
        /// <param name="context">Classe de contexto que herda da classe DbCOntext do Entity Framework</param>
        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método para adicionar dados no banco via Entity Framework
        /// </summary>
        /// <param name="obj">Objeto generico definido na herança da classe filha</param>
        public void Add(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);
            try
            {
                _context.SaveChanges();

            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        /// <summary>
        /// Método para adicionar uma lista de objetos de dados no banco via Entity Framework
        /// </summary>
        /// <param name="obj">Lista do Objeto generico definido na herança da classe filha</param>
        public void AddRange(IEnumerable<TEntity> obj)
        {
            _context.Set<TEntity>().AddRange(obj);
            _context.SaveChanges();
        }

        /// <summary>
        /// Método para adicionar uma lista de objetos de dados no banco via Entity Framework
        /// </summary>
        /// <param name="obj">Lista do Objeto generico definido na herança da classe filha</param>
        public TEntity GetById(object id)
        {
            try
            {
                return _context.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Método para obter uma lista de objetos de dados do banco via Entity Framework
        /// </summary>
        /// <returns>Retorna a tabela inteira do banco sem nenhuma condição</returns>
        public List<TEntity> GetAll(bool notracking = false)
        {
            if (notracking)
                return _context.Set<TEntity>().AsNoTracking().ToList();
            else
                return _context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// Método para obter uma lista de objetos de dados do banco via Entity Framework
        /// </summary>
        /// <param name="condicao">Expressão lambida para condicionar os dados que serão trazidos do banc com condiçãoo</param>
        /// <returns>Retorna dados do banco de acordo com a condição</returns>
        public List<TEntity> GetAll(Func<TEntity, bool> condicao, bool notracking = false)
        {
            if (notracking)
                return _context.Set<TEntity>().AsNoTracking().AsQueryable().Where(condicao).ToList();
            else
                return _context.Set<TEntity>().AsQueryable().Where(condicao).ToList();


        }
        /// <summary>
        /// Método para obter uma lista de objetos de dados do banco via Entity Framework com condição e paginação
        /// </summary>
        /// <param name="condicao">Expressão lambida para condicionar os dados que serão trazidos do banco</param>
        /// <param name="NumPage">Numero da pagina</param>
        /// <param name="ItensPerPage">Quantidade de itens na página</param>
        /// <returns>Retorna dados do banco de acordo com a condição a a paginação</returns>
        public List<TEntity> GetAll(Func<TEntity, bool> condicao, int NumPage, int ItensPerPage)
        {
            return _context.Set<TEntity>()
                .Where(condicao)
                .Skip((NumPage - 1) * ItensPerPage)
                .Take(ItensPerPage).ToList();
        }

        public IEnumerable<TEntity> GetAll(int NumPage, int ItensPerPage)
        {
            return _context.Set<TEntity>()
                .Skip((NumPage - 1) * ItensPerPage)
                .Take(ItensPerPage);
        }

        /// <summary>
        /// Método para alterar dados do banco via Entity Framework
        /// </summary>
        /// <param name="obj">Objeto generico definido na herança da classe filha</param>
        public void Update(TEntity obj)
        {
            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para remover dados do banco via Entity Framework
        /// </summary>
        /// <param name="obj">Objeto generico definido na herança da classe filha</param>
        public void Remove(object ID)
        {
            TEntity obj = GetById(ID);
            _context.Set<TEntity>().Remove(obj);
            _context.SaveChanges();
        }

        /// <summary>
        /// Método para remover dados do banco via Entity Framework - Remove vários objetos
        /// </summary>
        /// <param name="obj">Lista do Objeto generico definido na herança da classe filha</param>
        public void RemoveRange(IEnumerable<TEntity> obj)
        {
            _context.Set<TEntity>().RemoveRange(obj);
            _context.SaveChanges();
        }

        /// <summary>
        /// Método para verificar se existe uma determinada entidade no banco
        /// </summary>
        /// <param name="obj">Id da entidade</param>
        public bool Existe(object obj)
        {
            return _context.Set<TEntity>().Find(obj) != null;
        }

        public int GetMaxId(Func<TEntity, int> selector)
        {
            
            try { return _context.Set<TEntity>().Max(selector) + 1; }
            catch { return 1; }
        }

    }
}
