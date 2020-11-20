using Domain.Entity;
using Domain.Filtros;
using Domain.Function;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Busines
{
    public class FornecedorBll
    {
        Context context;
        FornecedorRepository rep;
        PessoaBll pessoaBll;

        public FornecedorBll()
        {
            context = new Context();
            rep = new FornecedorRepository(context);
            pessoaBll = new PessoaBll();
        }

        public Paginador<Fornecedor> Get(FiltroFornecedor filtro)
        {
            IQueryable<Fornecedor> query = context.Fornecedor;

            if (filtro.Id != 0)
            {
                query = query.Where(x => x.Id == filtro.Id);
            }

            if (filtro.Nome != null && filtro.Nome != "")
            {
                query = query.Where(x => x.Pessoa.Nome.Contains(filtro.Nome));
            }

            if (filtro.Cpf != null && filtro.Cpf != "")
            {
                query = query.Where(x => x.Pessoa.Cpf.Contains(filtro.Cpf));
            }

            if (filtro.Cnpj != null && filtro.Cnpj != "")
            {
                query = query.Where(x => x.Pessoa.Cnpj.Contains(filtro.Cnpj));
            }

            if (filtro.Rg != null && filtro.Rg != "")
            {
                query = query.Where(x => x.Pessoa.Rg.Contains(filtro.Rg));
            }

            if (filtro.Celular != null && filtro.Celular != "")
            {
                query = query.Where(x => x.Pessoa.Celular.Contains(filtro.Celular));
            }

            if (filtro.Email != null && filtro.Email != "")
            {
                query = query.Where(x => x.Pessoa.Email.Contains(filtro.Email));
            }

            if (filtro.Inativo == 0 || filtro.Inativo == 1)
            {
                query = query.Where(x => x.Pessoa.Inativo == filtro.Inativo);
            }

            int registros = query.Count();
            List<Fornecedor> fornecedores = query.ToList();
            Paginador<Fornecedor> dados = new Paginador<Fornecedor>();
            dados.Pagina = filtro.Pagina;
            dados.QtdeItensTotal = registros;
            dados.ItensPorPagina = filtro.ItensPorPagina;
            dados.QtdedePaginas = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(registros) / filtro.ItensPorPagina));
            dados.Dados = fornecedores;

            return dados;
        }

        public void Inativar(int id, int status)
        {
            pessoaBll.Inativar(id, status);
        }

        public List<Fornecedor> Get()
        {
            return rep.GetAll().OrderBy(x => x.Pessoa.Nome).ToList(); ;
        }

        public Fornecedor Get(int id)
        {
            return rep.GetById(id);
        }

        public Fornecedor Add(Fornecedor fornecedor)
        {
            fornecedor.Pessoa.Inativo = 0;

            pessoaBll.FormatarDados(fornecedor.Pessoa);

            pessoaBll.ValidaDadosPessoais(fornecedor.Pessoa);

            fornecedor.Id = rep.getMaxId();
            fornecedor.Pessoa.Id = rep.getMaxIdPessoa();
            fornecedor.Pessoa.DataCadastro = DateTime.Now;
            rep.Add(fornecedor);

            return fornecedor;
        }

        public void Update(Fornecedor fornecedor)
        {
            pessoaBll.Update(fornecedor.Pessoa);
        }

    }


}
