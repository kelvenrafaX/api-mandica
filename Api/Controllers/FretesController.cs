using Busines;
using Domain.Entity;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class FretesController : ApiController
    {
        FretesBll busines = new FretesBll();

        [HttpGet, Route("api/fretes")]
        public JsonResult<List<Fretes>> Get()
        {
            return Json(busines.Get());
        }

        [HttpGet, Route("api/fretes/{id}")]
        public JsonResult<Fretes> Get(int id)
        {
            return Json(busines.Get(id));
        }

        [HttpPost, Route("api/fretes")]
        public JsonResult<JsonResponse<Fretes>> Post([FromBody]Fretes fretes)
        {
            try
            {
                busines.Add(fretes);
                return Json(new JsonResponse<Fretes> { Type = "success", Title = "Fretes!", Message = "Sucesso ao cadastrar o frete!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Fretes> { Type = "error", Title = "Fretes!", Message = ex.Message });
            }
        }

        [HttpPost, Route("api/fretes/AddLista")]
        public JsonResult<JsonResponse<Fretes>> AddLista([FromBody]List<Fretes> fretes)
        {
            try
            {
                busines.Add(fretes);
                return Json(new JsonResponse<Fretes> { Type = "success", Title = "Fretes!", Message = "Sucesso ao cadastrar o frete!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Fretes> { Type = "error", Title = "Fretes!", Message = ex.Message });
            }
        }

        [HttpPut, Route("api/fretes")]
        public JsonResult<JsonResponse<Fretes>> Put([FromBody]Fretes fretes)
        {
            try
            {
                busines.Update(fretes);
                return Json(new JsonResponse<Fretes> { Type = "success", Title = "Fretes!", Message = "Sucesso ao editar o frete!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Fretes> { Type = "error", Title = "Fretes!", Message = ex.Message });
            }
        }

        [HttpDelete, Route("api/fretes/{id}")]
        public string Delete(int id)
        {
            return busines.Delete(id);
        }
    }
}
