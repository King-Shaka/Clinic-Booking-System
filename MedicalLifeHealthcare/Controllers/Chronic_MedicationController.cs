using MedicalLifeHealthcare.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedicalLifeHealthcare.Controllers
{
    public class Chronic_MedicationController : Controller
    {

        private readonly ApplicationDbContext _context;

        public Chronic_MedicationController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Chronic_MedicationController
        public ActionResult Doctor()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.Pre = _context.Prescription.Include(p => p.Doctor).Include(p => p.Patient).ToList();
            return View();
        }

        // GET: Chronic_MedicationController/Details/5
        public ActionResult Nurse()
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

        // GET: Chronic_MedicationController/Create
        public ActionResult Patient()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.Pre = _context.Prescription.Include(p => p.Doctor).Include(p => p.Patient).Where(a => a.PatientId == user).ToList();
            return View();
        }

    }
}
