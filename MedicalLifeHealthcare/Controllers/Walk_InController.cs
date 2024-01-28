using MedicalLifeHealthcare.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedicalLifeHealthcare.Controllers
{
	public class Walk_InController : Controller
	{
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        public Walk_InController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        public IActionResult Nurse()
		{
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.Appointmnet = _context.Appointments.Include(a => a.MainUser).OrderByDescending(a => a.CreatedAt).ToList();
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
            ViewBag.Appointmnet =  _context.Appointments.Include(a => a.MainUser).Where(e => e.PatientID == user).OrderByDescending(a => a.CreatedAt).ToList();
            return View();
		}
		public IActionResult Doctor()
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
