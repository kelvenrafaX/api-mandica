using Busines;
using Domain.Entity;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class CategoriaController : ApiController
    {
        CategoriaBll busines = new CategoriaBll();

        [HttpGet, Route("api/categoria")]
        public JsonResult<List<Categoria>> Get()
        {
            return Json(busines.Get());
        }

        [HttpGet, Route("api/categoria/PorTipo")]
        public JsonResult<List<Categoria>> PorTipo(Tipo tipo)
        {
            switch (tipo)
            {
                case Tipo.ACERVO:
                    return Json(busines.GetByType("ACERVO"));
                    break;
                case Tipo.PRODUTO:
                    return Json(busines.GetByType("PRODUTO"));
                    break;
                case Tipo.SERVICO:
                    return Json(busines.GetByType("SERVIÇO"));
                    break;
                case Tipo.TODOS:
                    return Json(busines.Get());
                    break;
                default:
                    return Json(busines.Get());
                    break;
            }
            
        }

        [HttpGet, Route("api/categoria/{id}")]
        public JsonResult<Categoria> Get(int id)
        {
            return Json(busines.Get(id));
        }

        [HttpPost, Route("api/categoria")]
        public JsonResult<JsonResponse<Categoria>> Post([FromBody]Categoria categoria)
        {
            try
            {
                Categoria entity = busines.Add(categoria);
                return Json(new JsonResponse<Categoria> { Type = "success", Title = "Categoria!", Message = "Sucesso ao cadastrar a categoria!", Entity = entity });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Categoria> { Type = "error", Title = "Categoria!", Message = ex.Message });
            }
        }

        [HttpPut, Route("api/categoria")]
        public JsonResult<JsonResponse<Categoria>> Put([FromBody]Categoria categoria)
        {
            try
            {
                busines.Update(categoria);
                return Json(new JsonResponse<Categoria> { Type = "success", Title = "Categoria!", Message = "Sucesso ao editar a categoria!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Categoria> { Type = "error", Title = "Categoria!", Message = ex.Message });
            }
        }

        [HttpDelete, Route("api/categoria/{id}")]
        public JsonResult<JsonResponse<Categoria>> Delete(int id)
        {
            try
            {
                busines.Delete(id);
                return Json(new JsonResponse<Categoria> { Type = "success", Title = "Categoria!", Message = "Sucesso ao remover a categoria!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Categoria> { Type = "error", Title = "Categoria!", Message = ex.Message });
            }
        }
    }
}
