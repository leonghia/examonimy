using Microsoft.AspNetCore.Mvc;

namespace ExamonimyWeb.Controllers;

[Route("")]
public class ExamController : Controller
{

    [HttpGet("exam")]
    public IActionResult RenderIndexView()
    {
        
        return View();
    }
}
