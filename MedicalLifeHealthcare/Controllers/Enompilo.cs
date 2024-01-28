using MedicalLifeHealthcare.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedicalLifeHealthcare.Controllers
{
    [Authorize]
    public class Enompilo : Controller
    {
        private readonly ApplicationDbContext _context;
        public Enompilo(ApplicationDbContext context)
        {
            _context = context;
           
        }

        public IActionResult Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
                if (Alerts.Count > 0)
                {
                    ViewBag.Alerts = Alerts;
                    TempData["Alerts"] = "Not null";
                }
                var File = _context.Medical_File.Where(a => a.PatientID == user).FirstOrDefault();
                if (File != null)
                {
                    TempData["FileFound"] = "Found";   
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public IActionResult Administrator()
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
        public IActionResult Pathology()
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
