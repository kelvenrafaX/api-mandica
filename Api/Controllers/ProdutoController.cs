using Busines;
using Domain.Entity;
using Domain.Filtros;
using Domain.Function;
using Domain.Helpers;
using System;
using System.Web.Http;
using System.Web.Http.Results;

namespace ApiLocacao.Controllers
{
    public class ProdutoController : ApiController
    {
        ProdutoBll busines = new ProdutoBll();

        [HttpGet, Route("api/produto/{id}")]
        public JsonResult<Produto> Get(int id, Tipo tipo)
        {
            return Json(busines.Get(id, tipo));
        }

        [HttpGet, Route("api/produto")]
        public JsonResult<Paginador<Produto>> Get(Tipo tipo)
        {
            Paginador<Produto> paginador = new Paginador<Produto>
            {
                Dados = busines.Get(tipo)
            };
            return Json(paginador);
        }

        [HttpPost, Route("api/produto/Filtro")]
        public JsonResult<Paginador<Produto>> Filtro(FiltroProduto filtro, Tipo tipo)
        {
            if (filtro.Pagina == 0) { filtro.Pagina = 1; }
            if (filtro.ItensPorPagina == 0) { filtro.ItensPorPagina = 20; }

            return Json(busines.GetAll(filtro, tipo));
        }

        [HttpPost, Route("api/produto/Inativar/{id}/{status}")]
        public JsonResult<JsonResponse<Produto>> Inativar(int id, int status)
        {
            try
            {
                busines.Inativar(id, status);
                return Json(new JsonResponse<Produto> { Type = "success", Title = "Produto!", Message = "Sucesso ao " + (status == 0 ? "Ativar" : "Inativar") + " o Produto!" });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Produto> { Type = "error", Title = "Produto!", Message = ex.Message });
            }
        }

        [HttpPost, Route("api/produto/{estoque}")]
        public JsonResult<JsonResponse<Produto>> Post(Produto produto, int estoque, Tipo tipo)
        {
            try
            {
                Produto entity = busines.Add(produto, tipo, estoque);
                return Json(new JsonResponse<Produto> { Type = "success", Title = tipo.ToString() + "!", Message = $"Sucesso ao cadastrar o {tipo.ToString()}!", Entity = entity });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Produto> { Type = "error", Title = tipo.ToString() + "!", Message = ex.Message });
            }
        }

        [HttpPut, Route("api/produto/{estoque}")]
        public JsonResult<JsonResponse<Produto>> Put([FromBody]Produto produto, int estoque, Tipo tipo)
        {
            try
            {
                Produto entity = busines.Update(produto, estoque);
                return Json(new JsonResponse<Produto> { Type = "success", Title = tipo.ToString() + "!", Message = $"Sucesso ao editar o {tipo.ToString()}!", Entity = entity });
            }
            catch (Exception ex)
            {
                return Json(new JsonResponse<Produto> { Type = "error", Title = tipo.ToString() + "!", Message = ex.Message });
            }
        }

        [HttpDelete, Route("api/produto/{id}")]
        public void Delete(int id)
        {
        }

    }
}
