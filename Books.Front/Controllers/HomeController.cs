using System.Web.Mvc;

namespace Books.Front.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Authors");
        }
    }
}
