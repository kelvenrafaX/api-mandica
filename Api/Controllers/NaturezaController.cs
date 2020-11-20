using Busines;
using Domain.Entity;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class NaturezaController : ApiController
    {
        NaturezaBll busines = new NaturezaBll();

        [HttpGet, Route("api/natureza")]
        public JsonResult<List<Natureza>> Get()
        {
            return Json(busines.Get());
        }

        [HttpGet, Route("api/natureza/{id}")]
        public JsonResult<Natureza> Get(int id)
        {
            return Json(busines.Get(id));
        }

        [HttpPost, Route("api/natureza")]
        public JsonResult<JsonResponse<Natureza>> Post([FromBody]Natureza natureza)
        {
            try
            {
                busines.Add(natureza);
                return Json(new JsonResponse<Natureza> { Type = "success", Title = "Natureza!", Message = "Sucesso ao cadastrar a natureza!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Natureza> { Type = "error", Title = "Natureza!", Message = ex.Message });
            }
        }

        [HttpPut, Route("api/natureza")]
        public JsonResult<JsonResponse<Natureza>> Put([FromBody]Natureza natureza)
        {
            try
            {
                busines.Update(natureza);
                return Json(new JsonResponse<Natureza> { Type = "success", Title = "Natureza!", Message = "Sucesso ao editar a natureza!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Natureza> { Type = "error", Title = "Natureza!", Message = ex.Message });
            }
        }

        [HttpDelete, Route("api/natureza/{id}")]
        public string Delete(int id)
        {
            return busines.Delete(id);
        }
    }
}
