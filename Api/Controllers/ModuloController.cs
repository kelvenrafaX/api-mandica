using Busines;
using Domain.Entity;
using System.Collections.Generic;
using System.Web.Http;

namespace ApiLocacao.Controllers
{
    public class ModuloController : ApiController
    {
        // GET api/values
        ModuloBll busines =  new ModuloBll();

        public IEnumerable<Modulos> Get()
        {
            var x = busines.Get();
            return x;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost, Route("api/usuario")]
        public void Post([FromBody]UsuarioAcesso value)
        {

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
