using MedicalLifeHealthcare.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedicalLifeHealthcare.Controllers
{
    public class PathologyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PathologyController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Test = _context.TestRequest.Include(t => t.Clinician).Include(t => t.Patient).ToList();
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            return View();
        }
        public IActionResult Patient()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            var applicationDbContext = _context.SampleResults.Include(s => s.Pathology).Include(s => s.Samples).Include(s => s.Samples.TestRequest).Where(a => a.Samples.TestRequest.PatientId == user);
            return View( applicationDbContext.ToList());
        }
        public IActionResult Nurse()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
          
            ViewBag.Test = _context.TestRequest.Include(t => t.Clinician).Include(t => t.Patient).ToList();
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            return View();
        }
    }
}
