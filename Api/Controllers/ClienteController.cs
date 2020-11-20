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
    public class ClienteController : ApiController
    {
        ClienteBll busines = new ClienteBll();
        
        [HttpGet, Route("api/cliente")]
        public JsonResult<List<Cliente>> Get()
        {
            return Json(busines.Get());
        }

        [HttpPost, Route("api/cliente/Filtro")]
        public JsonResult<Paginador<Cliente>> Filtro([FromBody] FiltroCliente filtro)
        {
            if (filtro.Pagina == 0) { filtro.Pagina = 1; }
            if (filtro.ItensPorPagina == 0) { filtro.ItensPorPagina = 20; }

            return Json(busines.Get(filtro));
        }

        [HttpGet, Route("api/cliente/{id}")]
        public JsonResult<Cliente> Get(int id)
        {
            return Json(busines.Get(id));
        }

        [HttpGet, Route("api/cliente/VerificaCPFCNPJ/{cpfCnpj}")]
        public JsonResult<JsonResponse<Cliente>> VerificaCPFCNPJ(string cpfCnpj)
        {
            try
            {
                new PessoaBll().VerificaCPFCNPJ(new Pessoa { Id = 0, Cpf = cpfCnpj, Cnpj = cpfCnpj });
                return Json(new JsonResponse<Cliente> { Type = "success", Title = "Cliente!", Message = "Cpf/Cnpj válido!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Cliente> { Type = "error", Title = "Cliente!", Message = ex.Message });
            }
        }

        [HttpPost, Route("api/cliente")]        
        public JsonResult<JsonResponse<Cliente>> Post([FromBody] Cliente cliente)
        {
            try
            {
                busines.Add(cliente);
                return Json(new JsonResponse<Cliente> { Type = "success", Title = "Cliente!", Message = "Sucesso ao cadastrar o cliente!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Cliente> { Type = "error", Title = "Cliente!", Message = ex.Message });
            }
        }

        [HttpPost, Route("api/cliente/AddLista")]
        public JsonResult<JsonResponse<Cliente>> AddLista([FromBody] List<Cliente> clientes)
        {
            try
            {
                busines.Add(clientes);
                return Json(new JsonResponse<Cliente> { Type = "success", Title = "Cliente!", Message = "Sucesso ao cadastrar os clientes!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Cliente> { Type = "error", Title = "Cliente!", Message = ex.Message });
            }
        }

        [HttpPost, Route("api/cliente/Inativar/{id}/{status}")]
        public JsonResult<JsonResponse<Cliente>> Inativar(int id, int status)
        {
            try
            {
                busines.Inativar(id, status);
                return Json(new JsonResponse<Cliente> { Type = "success", Title = "Cliente!", Message = "Sucesso ao " + (status == 0 ? "Ativar" : "Inativar") + " o cliente!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Cliente> { Type = "error", Title = "Cliente!", Message = ex.Message });
            }
        }

        [HttpPut, Route("api/cliente")]
        public JsonResult<JsonResponse<Cliente>> Put([FromBody]Cliente cliente)
        {
            try
            {
                busines.Update(cliente);
                return Json(new JsonResponse<Cliente> { Type = "success", Title = "Cliente!", Message = "Sucesso ao editar o cliente!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Cliente> { Type = "error", Title = "Cliente!", Message = ex.Message });
            }
        }

        [HttpDelete, Route("api/cliente")]
        public string Delete(int id)
        {
            return busines.Delete(id);
        }
    }
}
