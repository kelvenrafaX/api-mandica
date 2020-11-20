using Busines;
using System.Web.Mvc;

namespace ApiLocacao.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
         {
            //metodo apenas para iniciar a migração;
            // ViewBag.Cliente = new ClienteBll().Get();
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
