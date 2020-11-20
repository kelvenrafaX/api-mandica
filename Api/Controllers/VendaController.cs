using Busines;
using Domain.Entity;
using Domain.Helpers;
using System;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class VendaController : ApiController
    {
        VendaBll busines = new VendaBll();

        [HttpGet, Route("api/venda/pagarParcela/{id}/{grupo}/{parcela}")]
        public JsonResult<JsonResponse<Venda>> PagarParcela(int id, int grupo, int parcela)
        {
            try
            {
                busines.PagarParcela(id, grupo, parcela);
                return Json(new JsonResponse<Venda> { Type = "success", Title = "Venda!", Message = "Sucesso ao pagar a parcela!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Venda> { Type = "error", Title = "Venda!", Message = ex.Message });
            }
        }

        [HttpPost, Route("api/venda/pagarTotal")]
        public JsonResult<JsonResponse<Venda>> PagarTotal(Orcamento orcamento)
        {
            try
            {
                busines.PagarTotal(orcamento);
                return Json(new JsonResponse<Venda> { Type = "success", Title = "Venda!", Message = "Sucesso ao pagar a Venda!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Venda> { Type = "error", Title = "Venda!", Message = ex.Message });
            }
        }
    }
}
