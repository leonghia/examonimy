using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers;

[Route("")]
public class ExamController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
