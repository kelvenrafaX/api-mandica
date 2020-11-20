using Domain.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Busines
{
    public class CargoBll
    {
        Context context;
        CargoRepository rep;

        public CargoBll()
        {
            context = new Context();
            rep = new CargoRepository(context);
        }

        public Cargo Get(int id)
        {

            var cargo = rep.GetById(id);

            return cargo;

        }
   
        public List<Cargo> Get()
        {
            
            return rep.GetAll().OrderByDescending(x => x.Ativa).ToList();
        }
          

        public void Add(Cargo cargo)
        {
            cargo.Descricao = cargo.Descricao.ToUpper();

            Validar(cargo);

            cargo.Id = rep.GetMaxId(x => x.Id);
            rep.Add(cargo);
        }

        public void Delete(int id)
        {
            Cargo cargo = rep.GetById(id);
            cargo.Ativa = false;
            rep.Update(cargo);
        }

        public void Update(Cargo cargo)
        {
            Validar(cargo);
            rep.Update(cargo);
        }

        private void Validar(Cargo cargo)
        {
            if (cargo.Descricao.Trim().Length == 0)
            {
                throw new Exception("O campo nome da categoria é obrigatório.");
            }
            if (context.Cargo.Where(x => x.Descricao == cargo.Descricao).Count() > 0) {
                throw new Exception("Já existe um cargo com a mesma descrição cadastrado.");
            }
        }

        public void MudarStatus(Cargo cargo)
        {         
            cargo.Ativa = !cargo.Ativa;
            rep.Update(cargo);
        }
    }
}
