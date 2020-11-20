using Domain.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Busines
{
    public class NaturezaBll
    {
        NaturezaRepository rep = new NaturezaRepository(new Context());
        NaturezaParcelasRepository repParcelas = new NaturezaParcelasRepository(new Context());
        readonly Context context = new Context();
        public Natureza Get(int id)
        {

            return rep.GetById(id);
        }

        public List<Natureza> Get()
        {
            return rep.GetAll();
        }


        public void Add(Natureza natureza)
        {
            // natureza.Descricao = natureza.Descricao.ToUpper();
            natureza.Id = rep.GetMaxId(x => x.Id);
            ValidarNatureza(natureza);
            
            rep.Add(natureza);
        }

        public void ValidarNatureza(Natureza natureza)
        {
            if (natureza.Descricao.Length <= 0)
            {
                throw new Exception("Campo descrição não pode estar vazio!");
            }
            else if (rep.GetAll().Where<Natureza>(x => x.Id != natureza.Id && x.Descricao == natureza.Descricao).Count() > 0)
            {
                throw new Exception("Já existe uma natureza com essa Descrição!");
            }
        }

        public string Delete(int id)
        {
            try
            {
                rep.Remove(id);
                return "Natureza com id " + id + " removido com sucesso!";
            }
            catch (Exception ex)
            {
                return "Falha ao remover natureza! " + ex;
            }

        }

        public void Update(Natureza natureza)
        {
            // natureza.Descricao = natureza.Descricao.ToUpper();
            ValidarNatureza(natureza);

            Natureza nat = rep.GetById(natureza.Id);
            nat.Descricao = natureza.Descricao;
            nat.Ativa = natureza.Ativa;
            UpdateNaturezaParcelas(natureza);
            rep.Update(nat);
        }

        public void UpdateNaturezaParcelas(Natureza natureza)
        {
            foreach (var item in natureza.NaturezaParcelas)
            {
                List<NaturezaParcelas> aux = repParcelas.GetAll(x => x.NaturezaId == item.NaturezaId && x.Parcela == item.Parcela).ToList();
                if(aux.Count > 0)
                {
                    aux[0].Ativa = item.Ativa;
                    aux[0].DiasVencimento = item.DiasVencimento;
                    aux[0].Tarifacao = item.Tarifacao;
                    repParcelas.Update(aux[0]);
                } else
                {
                    repParcelas.Add(item);
                }
            }
        }
    }
}
