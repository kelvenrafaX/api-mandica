using Busines;
using Domain.Entity;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class BandeiraController : ApiController
    {
        BandeiraBll busines = new BandeiraBll();

        [HttpGet, Route("api/bandeira")]
        public JsonResult<List<Bandeira>> Get()
        {
            return Json(busines.Get());
        }

        [HttpGet, Route("api/bandeira/{id}")]
        public JsonResult<Bandeira> Get(int id)
        {
            return Json(busines.Get(id));
        }

        [HttpPost, Route("api/bandeira")]
        public JsonResult<JsonResponse<Bandeira>> Post([FromBody]Bandeira bandeira)
        {
            try
            {
                busines.Add(bandeira);
                return Json( new JsonResponse<Bandeira> { Type = "success", Title = "Bandeira!", Message = "Sucesso ao cadastrar a bandeira!" } );
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Bandeira> { Type = "error", Title = "Bandeira!", Message = ex.Message });
            }
        }

        [HttpPut, Route("api/bandeira")]
        public JsonResult<JsonResponse<Bandeira>> Put([FromBody]Bandeira bandeira)
        {
            try
            {
                busines.Update(bandeira);
                return Json(new JsonResponse<Bandeira> { Type = "success", Title = "Bandeira!", Message = "Sucesso ao editar a bandeira!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Bandeira> { Type = "error", Title = "Bandeira!", Message = ex.Message });
            }
        }

        [HttpDelete, Route("api/bandeira/{id}")]
        public string Delete(int id)
        {
            return busines.Delete(id);
        }
    }
}
