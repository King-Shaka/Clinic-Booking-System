using MedicalLifeHealthcare.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedicalLifeHealthcare.Controllers
{
    public class CounsellingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CounsellingController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: CounsellingController
        public ActionResult Counsellor()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.Sessions =  _context.Counselling_Sessions.Include(c => c.Appointment).Include(c => c.Counsellor).Include(a => a.Appointment.MainUser).ToList();
            return View();
        }

        // GET: CounsellingController/Details/5
        public ActionResult Patient()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            ViewBag.Sessions = _context.Counselling_Sessions.Include(c => c.Appointment).Include(c => c.Counsellor).Include(a => a.Appointment.MainUser).Where(a => a.Appointment.PatientID == user).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            return View();
        }
    }
}
