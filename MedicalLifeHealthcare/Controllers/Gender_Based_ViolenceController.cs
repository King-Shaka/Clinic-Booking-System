using MedicalLifeHealthcare.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedicalLifeHealthcare.Controllers
{
    public class Gender_Based_ViolenceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Gender_Based_ViolenceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public ActionResult Doctor()
        {
            ViewBag.Rep = _context.IncidentReport.Include(i => i.Patient).ToList();
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            return View();
        }

        // GET: Gender_Based_ViolenceController/Create
        public ActionResult Patient()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.Rep = _context.IncidentReport.Include(i => i.Patient).Where(a => a.PatientID == user).ToList();
            return View();
        }
         public ActionResult Social_Worker()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
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
