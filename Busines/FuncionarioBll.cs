using Domain.Entity;
using Domain.Filtros;
using Domain.Function;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Busines
{
    public class FuncionarioBll
    {
        Context context;
        FuncionarioRepository rep;
        PessoaBll pessoaBll;

        public FuncionarioBll()
        {
            context = new Context();
            rep = new FuncionarioRepository(context);
            pessoaBll = new PessoaBll();
        }

        public Paginador<Funcionario> Get(FiltroFuncionario filtro)
        {
            IQueryable<Funcionario> query = context.Funcionario;

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
            List<Funcionario> funcionarios = query.ToList();
            Paginador<Funcionario> dados = new Paginador<Funcionario>();
            dados.Pagina = filtro.Pagina;
            dados.QtdeItensTotal = registros;
            dados.ItensPorPagina = filtro.ItensPorPagina;
            dados.QtdedePaginas = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(registros) / filtro.ItensPorPagina));
            dados.Dados = funcionarios;

            return dados;
        }

        public List<Cargo> GetCargosFuncionario()
        {
            return context.Cargo.Where(x => x.Ativa == true).ToList();
        }

        public void Inativar(int id, int status)
        {
            pessoaBll.Inativar(id, status);
        }

        public List<Funcionario> Get()
        {
            return rep.GetAll();
        }

        public void Add(Funcionario funcionario)
        {
            pessoaBll.FormatarDados(funcionario.Pessoa);

            pessoaBll.ValidaDadosPessoais(funcionario.Pessoa);
            
            funcionario.Id = rep.GetMaxId();
            funcionario.Pessoa.Id = rep.GetMaxIdPessoa();
            funcionario.Pessoa.DataCadastro = DateTime.Now;
            rep.Add(funcionario);
        }

        public void Update(Funcionario funcionario)
        {
            pessoaBll.FormatarDados(funcionario.Pessoa);

            pessoaBll.Update(funcionario.Pessoa);
        }
    }
}
