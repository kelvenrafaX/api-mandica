using Busines;
using Domain.Entity;
using Domain.Filtros;
using Domain.Function;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class OrcamentoController : ApiController
    {
        OrcamentoBll busines = new OrcamentoBll();

        [HttpGet, Route("api/orcamento")]
        public JsonResult<List<Orcamento>> Get()
        {
            return Json(busines.Get());
        }

        [HttpGet, Route("api/orcamento/{id}")]
        public JsonResult<Orcamento> Get(int id)
        {
            return Json(busines.Get(id));
        }

        [HttpPost, Route("api/orcamento/Filtro")]
        public JsonResult<Paginador<Orcamento>> Filtro([FromBody] FiltroOrcamento filtro)
        {
            if (filtro.Pagina == 0) { filtro.Pagina = 1; }
            if (filtro.ItensPorPagina == 0) { filtro.ItensPorPagina = 20; }

            return Json(busines.Get(filtro));
        }

        [HttpGet, Route("api/orcamento/GetLastOrder")]
        public int GetIdLastOrder()
        {
            return busines.GetIdLastOrder();
        }

        [HttpPost, Route("api/orcamento")]
        public JsonResult<JsonResponse<Orcamento>> Post([FromBody]Orcamento orcamento)
        {
            try
            {
                busines.Add(orcamento);
                return Json(new JsonResponse<Orcamento> { Type = "success", Title = $"{orcamento.TipoPedido}!", Message = $"Sucesso ao cadastrar o {orcamento.TipoPedido}!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Orcamento> { Type = "error", Title = $"{orcamento.TipoPedido}!", Message = ex.Message });
            }
        }

        [HttpPost, Route("api/orcamento/AtualizaStatus")]
        public JsonResult<JsonResponse<Orcamento>> AtualizaStatus([FromBody]Orcamento orcamento)
        {
            try
            {
                busines.AtualizaStatus(orcamento);
                return Json(new JsonResponse<Orcamento> { Type = "success", Title = "Orçamento!", Message = "Sucesso ao editar o Orçamento!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Orcamento> { Type = "error", Title = "Orçamento!", Message = ex.Message });
            }
        }

        [HttpPut, Route("api/orcamento")]
        public JsonResult<JsonResponse<Orcamento>> Put([FromBody]Orcamento orcamento)
        {
            try
            {
                busines.Update(orcamento);
                return Json(new JsonResponse<Orcamento> { Type = "success", Title = $"{orcamento.TipoPedido}!", Message = $"Sucesso ao atualizar o {orcamento.TipoPedido}!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Orcamento> { Type = "error", Title = $"{orcamento.TipoPedido}!", Message = ex.Message });
            }
        }

        [HttpDelete, Route("api/orcamento/{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost, Route("api/orcamento/SavePdf")]
        public void SavePdf()
        {
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["data"];
            var caminho = Path.Combine(HttpContext.Current.Server.MapPath("~/Imagens/Produtos"), "teste.pdf");
            postedFile.SaveAs(caminho);
        }
    }
}
