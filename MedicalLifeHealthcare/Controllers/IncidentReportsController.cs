using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalLifeHealthcare.Areas.Identity.Data;
using MedicalLifeHealthcare.Models;
using System.Security.Claims;

namespace MedicalLifeHealthcare.Controllers
{
    public class IncidentReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncidentReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IncidentReports
        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> All_GBV()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.Rep = _context.IncidentReport.Include(i => i.Patient).ToList();
            return View();
        }

        // GET: IncidentReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IncidentReport == null)
            {
                return NotFound();
            }

            var incidentReport = await _context.IncidentReport
                .Include(i => i.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incidentReport == null)
            {
                return NotFound();
            }

            return View(incidentReport);
        }

        // GET: IncidentReports/Create
        public IActionResult Create()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: IncidentReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReportDate,PatientID,Type,Description,Location,InciodentDate,ActionsTaken")] IncidentReport incidentReport)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                incidentReport.PatientID = user;
                _context.Add(incidentReport);
                await _context.SaveChangesAsync();
                TempData["Success"] = "New GBV Incident Report Has been Created Successfully";
                TempData["UpdateType"] = "success";
                var alerts = new Alert()
                {
                    Message = "New GBV Incident Report Has been Created Successfully",
                    IntendedUser = user,
                    Purpose = "Notification",
                };
                _context.Alerts.Add(alerts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", incidentReport.PatientID);
            return View(incidentReport);
        }

        // GET: IncidentReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id == null || _context.IncidentReport == null)
            {
                return NotFound();
            }

            var incidentReport = await _context.IncidentReport.FindAsync(id);
            if (incidentReport == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", incidentReport.PatientID);
            return View(incidentReport);
        }

        // POST: IncidentReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReportDate,PatientID,Type,Description,Location,InciodentDate,ActionsTaken")] IncidentReport incidentReport)
        {
            if (id != incidentReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incidentReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidentReportExists(incidentReport.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", incidentReport.PatientID);
            return View(incidentReport);
        }

        // GET: IncidentReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IncidentReport == null)
            {
                return NotFound();
            }

            var incidentReport = await _context.IncidentReport
                .Include(i => i.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incidentReport == null)
            {
                return NotFound();
            }
         
            if (incidentReport != null)
            {
                _context.IncidentReport.Remove(incidentReport);
            }
            await _context.SaveChangesAsync();
            TempData["Success"] = "GBV Incident Report Has been delete Successfully";
            TempData["UpdateType"] = "success";
            return RedirectToAction(nameof(Index));
        }

        // POST: IncidentReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IncidentReport == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IncidentReport'  is null.");
            }
            var incidentReport = await _context.IncidentReport.FindAsync(id);
            if (incidentReport != null)
            {
                _context.IncidentReport.Remove(incidentReport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidentReportExists(int id)
        {
          return (_context.IncidentReport?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
