using Busines;
using Domain.Entity;
using Domain.Helpers;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class UsuarioController : ApiController
    {
        // GET api/values

        UsuarioBll business = new UsuarioBll(); 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost, Route("api/usuario/validarAcesso")]
        public JsonResult<Funcionario> ValidarAcesso([FromBody]Login login)
        {
            return Json(business.ValidarAcesso(login));
    
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
