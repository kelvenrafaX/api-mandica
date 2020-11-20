using Business;
using Domain.Entity;
using Domain.Function;
using Domain.Helpers;
using System;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class CompraController : ApiController
    {
        CompraBll busines = new CompraBll();

        [HttpGet, Route("api/compra")]
        public JsonResult<Paginador<Compra>> Get()
        {
            return Json(busines.GetAll());
        }

        [HttpPost, Route("api/compra")]
        public JsonResult<JsonResponse<Compra>> Post([FromBody]Compra compra)
        {
            try
            {
                busines.Add(compra);
                return Json(new JsonResponse<Compra> { Type = "success", Title = "Compra!", Message = "Sucesso ao cadastrar a compra!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Compra> { Type = "error", Title = "Compra!", Message = ex.Message });
            }
        }
    }
}