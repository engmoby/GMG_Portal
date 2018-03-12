using System.Web.Mvc;

namespace Front.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ViewResult Index()
        {
            return View("Error");
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 500;  //you may want to set this to 200
            return View("NotFound");
        }
    }
}