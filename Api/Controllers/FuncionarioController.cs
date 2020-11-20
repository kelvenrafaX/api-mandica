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
    public class FuncionarioController : ApiController
    {
        FuncionarioBll busines = new FuncionarioBll();

        [HttpGet, Route("api/funcionario")]
        public JsonResult<List<Funcionario>> Get()
        {
            return Json(busines.Get());
        }

        [HttpGet, Route("api/funcionario/{id}")]
        public JsonResult<List<Funcionario>> Get(int id)
        {
            return Json(busines.Get());
        }

        [HttpGet, Route("api/funcionario/ObterCargos")]
        public JsonResult<List<Cargo>> ObterCargos()
        {
            return Json(busines.GetCargosFuncionario());
        }


        [HttpPost, Route("api/funcionario/Filtro")]
        public JsonResult<Paginador<Funcionario>> Filtro([FromBody] FiltroFuncionario filtro)
        {
            if (filtro.Pagina == 0) { filtro.Pagina = 1; }
            if (filtro.ItensPorPagina == 0) { filtro.ItensPorPagina = 20; }

            return Json(busines.Get(filtro));
        }

        [HttpPost, Route("api/funcionario/Inativar/{id}/{status}")]
        public JsonResult<JsonResponse<Funcionario>> Inativar(int id, int status)
        {
            try
            {
                busines.Inativar(id, status);
                return Json(new JsonResponse<Funcionario> { Type = "success", Title = "Funcionário!", Message = "Sucesso ao " + (status == 0 ? "Ativar" : "Inativar") + " o funcionário!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Funcionario> { Type = "error", Title = "Funcionário!", Message = ex.Message });
            }
        }

        [HttpGet, Route("api/funcionario/VerificaCPFCNPJ/{cpfCnpj}")]
        public JsonResult<JsonResponse<Funcionario>> VerificaCPFCNPJ(string cpfCnpj)
        {
            try
            {
                new PessoaBll().VerificaCPFCNPJ(new Pessoa { Id = 0, Cpf = cpfCnpj, Cnpj = cpfCnpj });
                return Json(new JsonResponse<Funcionario> { Type = "success", Title = "Funcionário!", Message = "Cpf/Cnpj Válido!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Funcionario> { Type = "error", Title = "Funcionário!", Message = ex.Message });
            }
        }

        [HttpPost, Route("api/funcionario")]
        public JsonResult<JsonResponse<Funcionario>> Post([FromBody]Funcionario funcionario)
        {
            try
            {
                busines.Add(funcionario);
                return Json(new JsonResponse<Funcionario> { Type = "success", Title = "Funcionário!", Message = "Sucesso ao cadastrar o funcionário!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Funcionario> { Type = "error", Title = "Funcionário!", Message = ex.Message });
            }
        }

        [HttpPut, Route("api/funcionario")]
        public JsonResult<JsonResponse<Funcionario>> Put([FromBody]Funcionario funcionario)
        {
            try
            {
                busines.Update(funcionario);
                return Json(new JsonResponse<Funcionario> { Type = "success", Title = "Funcionário!", Message = "Sucesso ao editar o funcionário!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Funcionario> { Type = "error", Title = "Funcionário!", Message = ex.Message });
            }
        }

        [HttpDelete, Route("api/funcionario/{id}")]
        public void Delete(int id)
        {
        }
    }
}
