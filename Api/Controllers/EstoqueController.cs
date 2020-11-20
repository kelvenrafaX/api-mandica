using Business;
using Domain.Entity;
using Domain.Function;
using System;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class EstoqueController : ApiController
    {
        EstoqueBll busines = new EstoqueBll();

        [HttpGet, Route("api/estoque")]
        public JsonResult<Paginador<Estoque>> Get()
        {
            return Json(busines.GetAll());
        }

        [HttpPost, Route("api/estoque/acervo")]
        public JsonResult<Paginador<Produto>> Acervo(DateTime DataInicial, DateTime DataFinal)
        {
            return Json(busines.GetAll(DataInicial, DataFinal));
        }
    }
}