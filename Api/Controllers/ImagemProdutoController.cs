using Busines;
using Domain.Entity;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;


namespace ApiLocacao.Controllers
{
    public class ImagemProdutoController : ApiController
    {

        ImagemProdutoBll busines;


        protected ImagemProdutoController()
        {
            busines = new ImagemProdutoBll();
        }

        [HttpGet, Route("api/ImagemProduto")]
        public List<ImagemProduto> Get()
        {
            return busines.Get();
        }

        [HttpPost, Route("api/ImagemProduto/Atualizar/{id}")]
        public void Atualizar(int id)
        {
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["imagem"];
            if (postedFile.ContentLength > 0)
            {
                //var idProduto = id;
                //var extensao = postedFile.FileName.Split('.');
                //var nomeArquivo = new ImagemProdutoRepository(new Context()).getMaxId() + "." + extensao[1];
                //new ImagemProdutoBll().AddOrUpdade(new ImagemProduto
                //{
                //    Id = busines.GetLastId(),
                //    Descricao = nomeArquivo,
                //    Referencia = idProduto.ToString()
                //});
                //if (System.IO.File.Exists("" + HttpRuntime.AppDomainAppPath + "/Imagens/Produtos" + nomeArquivo + ""))
                //{
                //    System.IO.File.Delete("" + HttpRuntime.AppDomainAppPath + "/Imagens/Produtos" + nomeArquivo + "");
                //}

                //var caminho = Path.Combine(HttpContext.Current.Server.MapPath("~/Imagens/Produtos"), nomeArquivo);
                //postedFile.SaveAs(caminho);
            }
        }

        [HttpPost, Route("api/ImagemProduto")]
        public void Post()
        {
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["imagem"];
            if (postedFile.ContentLength > 0)
            {
                //var idProduto = new ProdutoBll().GetIdMax();
                //var extensao = postedFile.FileName.Split('.');
                //var nomeArquivo = idProduto + "." + extensao[1];
                //new ImagemProdutoBll().AddOrUpdade(new ImagemProduto
                //{
                //    Id = busines.GetLastId(),
                //    Descricao = nomeArquivo,
                //    Referencia = idProduto.ToString()
                //});
                //if (System.IO.File.Exists("" + HttpRuntime.AppDomainAppPath + "/Imagens/Produtos" + nomeArquivo + ""))
                //{
                //    System.IO.File.Delete("" + HttpRuntime.AppDomainAppPath + "/Imagens/Produtos" + nomeArquivo + "");
                //}

                //var caminho = Path.Combine(HttpContext.Current.Server.MapPath("~/Imagens/Produtos"), nomeArquivo);
                //postedFile.SaveAs(caminho);
            }

        }
    }
}
