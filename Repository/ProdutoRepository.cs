using Domain.Entity;
using Domain.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public  class ProdutoRepository:BaseRepository<Produto>       
    {
        public ProdutoRepository(Context context)
            : base(context)
        {

        }

        public List<Produto> ObterProdutos(string v1, object p1, int v2, object p2, int v3, object p3, int v4, object p4, bool v5, object p5, int v6, object p6, int v7, object p7)
        {
            throw new NotImplementedException();
        }

        public Paginador<Produto> ObterProdutos(string referencia, int fornecedor, int categoria, int pagina, int itensPorPagina)
        {
                        var sql = "";

            sql += "Select Count(*) FROM Produto ";
            if (referencia.Length > 0)
                sql += " Where Id ='" + referencia + "'";
            if (fornecedor > 0)
                sql += " And FornecedorId =" + fornecedor;
            if (categoria > 0)
                sql += " And CategoriaId =" + categoria;

            int registros = _context.Database.SqlQuery<int>(sql).FirstOrDefault();

            sql = " DECLARE @PageNumber AS INT, @RowspPage AS INT";
            sql += " SET @PageNumber = " + pagina;
            sql += " SET @RowspPage = " + itensPorPagina;
            sql += " SELECT * FROM(";
            sql += "             SELECT ROW_NUMBER() OVER(ORDER BY Id) AS NUMBER,";
            sql += "                    * FROM Produto ";
            if (referencia.Length > 0)
                sql += " Where Id ='" + referencia + "'";
            if (fornecedor > 0)
                sql += " And FornecedorId =" + fornecedor;
            if (categoria > 0)
                sql += " And CategoriaId =" + categoria;
         
            sql += "               ) AS TBL";
            sql += " WHERE NUMBER BETWEEN((@PageNumber - 1) * @RowspPage + 1) AND(@PageNumber * @RowspPage)";
            sql += " ORDER BY Id";

            List<Produto> produtos = _context.Database.SqlQuery<Produto>(sql).ToList();

            Paginador<Produto> dados = new Paginador<Produto>();
            dados.Pagina = pagina;
            dados.ItensPorPagina = itensPorPagina;
            dados.QtdedePaginas = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(registros) / itensPorPagina));
            dados.Dados = produtos;

            return dados;
        }
    }
}
