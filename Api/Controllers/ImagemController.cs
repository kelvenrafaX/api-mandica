using Busines;
using Domain.Entity;
using Repository;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;


namespace ApiLocacao.Controllers
{
    public class ImagemController : ApiController
    {

        ImagemBll busines;


        protected ImagemController()
        {
            busines = new ImagemBll();
        }

        [HttpGet, Route("api/Imagem")]
        public List<Imagem> Get()
        {
            return busines.Get();
        }

        [HttpPost, Route("api/Imagem/Atualizar/{id}")]
        public void Atualizar(int id)
        {
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["imagem"];
            if (postedFile.ContentLength > 0)
            {
                var idProduto = id;
                var extensao = postedFile.FileName.Split('.');
                var nomeArquivo = new ImagemRepository(new Context()).GetMaxId() + "." + extensao[1];
                new ImagemBll().AddOrUpdade(new Imagem
                {
                    Id = busines.GetLastId(),
                    Descricao = nomeArquivo
                });
                if (System.IO.File.Exists("" + HttpRuntime.AppDomainAppPath + "/Imagens/Produtos" + nomeArquivo + ""))
                {
                    System.IO.File.Delete("" + HttpRuntime.AppDomainAppPath + "/Imagens/Produtos" + nomeArquivo + "");
                }

                var caminho = Path.Combine(HttpContext.Current.Server.MapPath("~/Imagens/Produtos"), nomeArquivo);
                postedFile.SaveAs(caminho);
            }
        }

        [HttpPost, Route("api/Imagem")]
        public void Post()
        {
            var httpRequest = HttpContext.Current.Request;
            var postedFile = httpRequest.Files["imagem"];
            if (postedFile.ContentLength > 0)
            {
                var nomeArquivo = postedFile.FileName;
                new ImagemBll().AddOrUpdade(new Imagem
                {
                    Id = busines.getMaxId(),
                    Descricao = nomeArquivo
                });
                if (System.IO.File.Exists("" + HttpRuntime.AppDomainAppPath + "/Imagens/Produtos" + nomeArquivo + ""))
                {
                    System.IO.File.Delete("" + HttpRuntime.AppDomainAppPath + "/Imagens/Produtos" + nomeArquivo + "");
                }

                var caminho = Path.Combine(HttpContext.Current.Server.MapPath("~/Imagens/Produtos"), nomeArquivo);
                postedFile.SaveAs(caminho);
            }

        }
    }
}
