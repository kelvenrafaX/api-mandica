using Busines;
using Domain.Entity;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class CargoController : ApiController
    {
        CargoBll busines = new CargoBll();

        [HttpGet, Route("api/cargo")]
        public JsonResult<List<Cargo>> Get()
        {
            return Json(busines.Get());
        }
        
        [HttpGet, Route("api/cargo/{id}")]
        public JsonResult<Cargo> Get(int id)
        {
            return Json(busines.Get(id));
        }

        [HttpPost, Route("api/cargo")]
        public JsonResult<JsonResponse<Cargo>> Post([FromBody]Cargo cargo)
        {
            try
            {
                busines.Add(cargo);
                return Json(new JsonResponse<Cargo> { Type = "success", Title = "cargo!", Message = "Sucesso ao cadastrar a cargo!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Cargo> { Type = "error", Title = "cargo!", Message = ex.Message });
            }
        }

        [HttpPost, Route("api/cargo/MudarStatus")]
        public JsonResult<JsonResponse<Cargo>> MudarStatus([FromBody]Cargo cargo)
        {
            try
            {
                busines.MudarStatus(cargo);
                return Json(new JsonResponse<Cargo> { Type = "success", Title = "cargo!", Message = "Sucesso ao cadastrar a cargo!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Cargo> { Type = "error", Title = "cargo!", Message = ex.Message });
            }
        }

        [HttpPut, Route("api/cargo")]
        public JsonResult<JsonResponse<Cargo>> Put([FromBody]Cargo cargo)
        {
            try
            {
                busines.Update(cargo);
                return Json(new JsonResponse<Cargo> { Type = "success", Title = "cargo!", Message = "Sucesso ao editar a cargo!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Cargo> { Type = "error", Title = "cargo!", Message = ex.Message });
            }
        }

        [HttpDelete, Route("api/cargo/{id}")]
        public void Delete(int id)
        {
             busines.Delete(id);
        }
    }
}
