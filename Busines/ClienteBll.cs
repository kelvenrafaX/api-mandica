using Domain.Entity;
using Domain.Filtros;
using Domain.Function;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Busines
{
    public class ClienteBll
    {
        Context context;
        ClienteRepository rep;
        PessoaBll pessoaBll;

        public ClienteBll()
        {
            context = new Context();
            rep = new ClienteRepository(context);
            pessoaBll = new PessoaBll();
        }

        public Cliente Get(int id)
        {
            return rep.GetById(id);
        }

        public List<Cliente> Get()
        {
            return rep.GetAll().OrderBy(x => x.Pessoa.Nome).ToList();
        }

        public Paginador<Cliente> Get(FiltroCliente filtro)
        {
            IQueryable<Cliente> query = context.Cliente;

            if(filtro.Id != 0)
            {
                query = query.Where(x => x.Id == filtro.Id);
            }

            if(filtro.Nome != null && filtro.Nome != "")
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
            List<Cliente> clientes = query.ToList();
            Paginador<Cliente> dados = new Paginador<Cliente>
            {
                Pagina = filtro.Pagina,
                QtdeItensTotal = registros,
                ItensPorPagina = filtro.ItensPorPagina,
                QtdedePaginas = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(registros) / filtro.ItensPorPagina)),
                Dados = clientes
            };

            return dados;
        }

        public void Add(List<Cliente> clientes)
        {
            int i = 0;
            foreach (var cliente in clientes)
            {
                cliente.Pessoa.Inativo = 0;

                pessoaBll.FormatarDados(cliente.Pessoa);

                // ValidarCliente(cliente);

                cliente.Id = rep.GetMaxId();
                cliente.Pessoa.DataCadastro = DateTime.Now;
                cliente.Pessoa.Id = rep.GetMaxIdPessoa() + i;
            }

            rep.AddRange(clientes);
            
        }

        public void Add(Cliente cliente)
        {
            cliente.Pessoa.Inativo = 0;

            pessoaBll.FormatarDados(cliente.Pessoa);

            ValidarCliente(cliente);

            cliente.Id = rep.GetMaxId();
            cliente.Pessoa.DataCadastro = DateTime.Now;
            cliente.Pessoa.Id = rep.GetMaxIdPessoa();
            rep.Add(cliente);
        }

        public string Delete(int id)
        {
            try
            {
                rep.Remove(id);
                return "Cliente com id " + id + " removido com sucesso!";
            }
            catch
            {
                return "Falha ao remover cliente!";
            }

        }

        public void Inativar(int id, int status)
        {
            pessoaBll.Inativar(id, status);
        }

        private void ValidarCliente(Cliente cliente)
        {
            pessoaBll.ValidaDadosPessoais(cliente.Pessoa);
        }

        public void Update(Cliente cliente)
        {
            pessoaBll.Update(cliente.Pessoa);
        }
    }
}
