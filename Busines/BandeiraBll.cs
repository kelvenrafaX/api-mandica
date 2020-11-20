using Domain.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Busines
{
    public class BandeiraBll
    {
        BandeiraRepository rep = new BandeiraRepository(new Context());

        public Bandeira Get(int id)
        {
            return rep.GetById(id);
        }

        public List<Bandeira> Get()
        {
            return rep.GetAll();
        }

        public void Add(Bandeira bandeira)
        {
            bandeira.Descricao = bandeira.Descricao.ToUpper();
            bandeira.Id = rep.GetMaxId(x => x.Id);

            ValidarBandeira(bandeira);

            rep.Add(bandeira);               
        }

        public void ValidarBandeira(Bandeira bandeira)
        {
            if (bandeira.Descricao.Trim().Length <= 0)
            {
                throw new Exception("Campo descrição não pode estar vazio!");
            }
            else if (rep.GetAll(x => x.Id != bandeira.Id && x.Descricao == bandeira.Descricao).Count() > 0)
            {
                throw new Exception("Já existe uma bandeira com essa Descrição!");
            }
        }

        public string Delete(int id)
        {
            try
            {
                rep.Remove(id);
                return "Bandeira com id " + id + " removido com sucesso!";
            }
            catch (Exception ex)
            {
                return "Falha ao remover bandeira! " + ex;
            }

        }

        public void Update(Bandeira bandeira)
        {
            bandeira.Descricao = bandeira.Descricao.ToUpper();
            ValidarBandeira(bandeira);

            Bandeira nat = rep.GetById(bandeira.Id);
            nat.Descricao = bandeira.Descricao;
            rep.Update(nat);
        }
    }
}
