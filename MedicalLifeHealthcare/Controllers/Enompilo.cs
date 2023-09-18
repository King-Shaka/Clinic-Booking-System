using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalLifeHealthcare.Controllers
{
    [Authorize]
    public class Enompilo : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Administrator()
        {
            return View();
        }
    }
}
