using Microsoft.AspNetCore.Mvc;

namespace KFM.MVCWebApp.Controllers
{
    public class FoodRequirementsV2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
