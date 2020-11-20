using Domain.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Busines
{
    public class FretesBll
    {
        FretesRepository rep = new FretesRepository(new Context());

        readonly Context context = new Context();
        public Fretes Get(int id)
        {

            return rep.GetById(id);
        }

        public List<Fretes> Get()
        {
            return rep.GetAll();
        }


        public void Add(Fretes fretes)
        {
            fretes.Id = rep.GetMaxId(x => x.Id);
            Validar(fretes);
            
            rep.Add(fretes);
        }

        public void Add(List<Fretes> fretes)
        {
            int i = 0;
            foreach (var item in fretes)
            {
                Validar(item);
                item.Id = rep.GetMaxId(x => x.Id) + i;
                i++;
            }           

            rep.AddRange(fretes);
        }

        public void Validar(Fretes fretes)
        {
            if (fretes.Nome.Length <= 0)
            {
                throw new Exception("Campo nome não pode estar vazio!");
            }
        }

        public string Delete(int id)
        {
            try
            {
                rep.Remove(id);
                return "Frete com id " + id + " removido com sucesso!";
            }
            catch (Exception ex)
            {
                return "Falha ao remover frete! " + ex;
            }

        }

        public void Update(Fretes fretes)
        {
            Validar(fretes);

            Fretes freteUpdate = rep.GetById(fretes.Id);
            freteUpdate.Valor = fretes.Valor;
            rep.Update(freteUpdate);
        }
    }
}
