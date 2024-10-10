using Microsoft.AspNetCore.Mvc;

namespace KFM.MVCWebApp.Controllers
{
    public class WaterRequirementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
