using Microsoft.AspNetCore.Mvc;

namespace KFM.MVCWebApp.Controllers;

public class PondsV2Controller : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
