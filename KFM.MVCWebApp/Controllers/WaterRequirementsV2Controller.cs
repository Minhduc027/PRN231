using Microsoft.AspNetCore.Mvc;

namespace KFM.MVCWebApp.Controllers
{
    public class WaterRequirementsV2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
