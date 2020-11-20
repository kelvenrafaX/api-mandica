using Domain.Entity;
using Domain.Filtros;
using Domain.Function;
using Repository;
using System;
using System.Linq;

namespace Busines
{
    public class PessoaBll
    {
        Context context;
        PessoaRepository rep;
        public PessoaBll()
        {
            context = new Context();
            rep = new PessoaRepository(context);
        }

        public void VerificaCPFCNPJ(Pessoa Pessoa)
        {
            if ((Pessoa.Cpf == null || Pessoa.Cpf.Trim() == "") && (Pessoa.Cnpj == null || Pessoa.Cnpj.Trim() == ""))
            {
                throw new Exception("Campo " + (Pessoa.Cpf == null || Pessoa.Cpf.Trim() == "" ? "Cpf" : "Cnpj") + " deve ser preenchido!");
            }
            else if (Pessoa.Cpf.Trim().Length == 11 || Pessoa.Cnpj.Trim().Length == 14)
            {
                if (Pessoa.Cpf.Trim().Length == 11)
                {
                    if (context.Pessoa.Where(x => x.Cpf == Pessoa.Cpf && x.Id != Pessoa.Id).Count() > 0)
                    {
                        throw new Exception("CPF já cadastrado, por favor tente novamente");
                    }
                }
                else
                {
                    if (context.Pessoa.Where(x => x.Cnpj == Pessoa.Cnpj && x.Id != Pessoa.Id).Count() > 0)
                    {
                        throw new Exception("CNPJ já cadastrado, por favor tente novamente");
                    }
                }
            }
            else
            {
                throw new Exception("O número de caracteres está incorreto!");
            }
            
        }

        public void ValidaDadosPessoais(Pessoa pessoa)
        {
            if (pessoa.Nome == null)
            {
                throw new Exception ("Campo nome deve ser preenchido!");
            }
            else if (pessoa.SiglaSexo == null)
            {
                throw new Exception("Item Sexo deve ser selecionado!");
            }
            else if (pessoa.Celular == null)
            {
                throw new Exception("Campo Celular deve ser preenchido!");
            }
            else if (pessoa.TipoPessoa == null)
            {
                throw new Exception("Item Tipo Pessoa deve ser selecionado!");
            }

            VerificaCPFCNPJ(pessoa);
        }

        public void ValidaEndereco(Endereco endereco)
        {
            if (endereco.Cep == null)
            {
                throw new Exception("Campo Cep deve ser preenchido!");
            }
            else if (endereco.Bairro == null)
            {
                throw new Exception("Campo Bairro deve ser preenchido!");
            }
            else if (endereco.Cidade == null)
            {
                throw new Exception("Campo Cidade deve ser preenchido!");
            }
            else if (endereco.Rua == null)
            {
                throw new Exception("Campo Rua deve ser preenchido!");
            }
            else if (endereco.Numero == 0)
            {
                throw new Exception("Campo Número deve ser preenchido!");
            }
        }

        public void FormatarDados(Pessoa pessoa)
        {
            pessoa.Celular = Utils.SomenteNumeros(pessoa.Celular);
            pessoa.Telefone = Utils.SomenteNumeros(pessoa.Telefone);
            pessoa.Cpf = Utils.SomenteNumeros(pessoa.Cpf);
            pessoa.Cnpj = Utils.SomenteNumeros(pessoa.Cnpj);
        }

        public void Update(Pessoa pessoa)
        {
            FormatarDados(pessoa);

            ValidaDadosPessoais(pessoa);

            rep.Update(pessoa);
        }

        public void Inativar(int id, int status)
        {
            rep.Inativar(id, status);
        }
    }
}
