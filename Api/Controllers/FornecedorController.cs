using Busines;
using Domain.Entity;
using Domain.Filtros;
using Domain.Function;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class FornecedorController : ApiController
    {
        FornecedorBll busines = new FornecedorBll();

        [HttpGet, Route("api/fornecedor")]
        public JsonResult<List<Fornecedor>> Get()
        {
            return Json(busines.Get());
        }

        [HttpGet, Route("api/fornecedor/{id}")]
        public JsonResult<Fornecedor> Get(int id)
        {
            return Json(busines.Get(id));
        }

        [HttpPost, Route("api/fornecedor/Filtro")]
        public JsonResult<Paginador<Fornecedor>> Filtro([FromBody] FiltroFornecedor filtro)
        {
            if (filtro.Pagina == 0) { filtro.Pagina = 1; }
            if (filtro.ItensPorPagina == 0) { filtro.ItensPorPagina = 20; }

            return Json(busines.Get(filtro));
        }

        [HttpPost, Route("api/fornecedor/Inativar/{id}/{status}")]
        public JsonResult<JsonResponse<Fornecedor>> Inativar(int id, int status)
        {
            try
            {
                busines.Inativar(id, status);
                return Json(new JsonResponse<Fornecedor> { Type = "success", Title = "Fornecedor!", Message = "Sucesso ao " + (status == 0 ? "Ativar" : "Inativar") + " o fornecedor!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Fornecedor> { Type = "error", Title = "Fornecedor!", Message = ex.Message });
            }
        }

        [HttpGet, Route("api/fornecedor/VerificaCPFCNPJ/{cpfCnpj}")]
        public JsonResult<JsonResponse<Fornecedor>> VerificaCPFCNPJ(string cpfCnpj)
        {
            try
            {

                new PessoaBll().VerificaCPFCNPJ(new Pessoa { Id = 0, Cpf = cpfCnpj, Cnpj = cpfCnpj});
                return Json(new JsonResponse<Fornecedor> { Type = "success", Title = "Fornecedor!", Message = "Cpf/Cnpj Válido!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Fornecedor> { Type = "error", Title = "Fornecedor!", Message = ex.Message });
            }
        }

        [HttpPost, Route("api/fornecedor")]
        public JsonResult<JsonResponse<Fornecedor>> Post([FromBody]Fornecedor fornecedor)
        {
            try
            {
                Fornecedor entity = busines.Add(fornecedor);
                return Json(new JsonResponse<Fornecedor> { Type = "success", Title = "Fornecedor!", Message = "Sucesso ao cadastrar o fornecedor!", Entity = entity });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Fornecedor> { Type = "error", Title = "Fornecedor!", Message = ex.Message });
            }
        }

        [HttpPut, Route("api/fornecedor")]
        public JsonResult<JsonResponse<Fornecedor>> Put([FromBody]Fornecedor fornecedor)
        {
            try
            {
                busines.Update(fornecedor);
                return Json(new JsonResponse<Fornecedor> { Type = "success", Title = "Fornecedor!", Message = "Sucesso ao editar o fornecedor!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Fornecedor> { Type = "error", Title = "Fornecedor!", Message = ex.Message });
            }
        }

        [HttpDelete, Route("api/fornecedor/{id}")]
        public void Delete(int id)
        {
        }
    }
}
