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
    public class CasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cases
        public async Task<IActionResult> Index(int? Id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if(Id == null)
            {
                ViewBag.List = _context.Case.Include(w => w.Doctor).Include(w => w.incidentReport).Include(a => a.incidentReport.Patient).ToList();
            }
            else
            {
                ViewBag.List = _context.Case.Include(w => w.Doctor).Include(w => w.incidentReport).Include(a => a.incidentReport.Patient).Where(a => a.IncidentReportId == Id).ToList();
            }
         
            return View();
        }
        public async Task<IActionResult> My_Case()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.List = _context.Case.Include(w => w.Doctor).Include(w => w.incidentReport).Include(a => a.incidentReport.Patient).Where(a => a.incidentReport.PatientID == user).ToList();
            return View();
        }

        // GET: Cases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Case == null)
            {
                return NotFound();
            }

            var @case = await _context.Case
                .Include(w => w.Doctor)
                .Include(w => w.incidentReport)
                .FirstOrDefaultAsync(m => m.CaseId == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // GET: Cases/Create
        public IActionResult Create()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewData["DoctorsID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["IncidentReportId"] = new SelectList(_context.IncidentReport, "Id", "Id");
            return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Case case2)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                case2.DoctorsID = user;
                var incident = _context.IncidentReport.Where(a => a.Id == case2.IncidentReportId).FirstOrDefault();
                incident.Status = "Case Opened";

                _context.Add(case2);
                await _context.SaveChangesAsync();
                TempData["Success"] = "New GBV Case  Has been Created Successfully";
                TempData["UpdateType"] = "success";
                var alerts = new Alert()
                {
                    Message = "New GBV case Has been Created by our doctor Successfully",
                    IntendedUser = incident.PatientID,
                    Purpose = "Notification",
                };
                _context.Alerts.Add(alerts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorsID"] = new SelectList(_context.Users, "Id", "Id", case2.DoctorsID);
            ViewData["IncidentReportId"] = new SelectList(_context.IncidentReport, "Id", "Id", case2.IncidentReportId);
            return View(case2);
        }

        // GET: Cases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Case == null)
            {
                return NotFound();
            }

            var @case = await _context.Case.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewData["DoctorsID"] = new SelectList(_context.Users, "Id", "Id", @case.DoctorsID);
            ViewData["IncidentReportId"] = new SelectList(_context.IncidentReport, "Id", "Id", @case.IncidentReportId);
            return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseId,DateOpened,IncidentReportId,Status,DoctorsID,Notes")] Case @case)
        {
            if (id != @case.CaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@case);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaseExists(@case.CaseId))
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
            ViewData["DoctorsID"] = new SelectList(_context.Users, "Id", "Id", @case.DoctorsID);
            ViewData["IncidentReportId"] = new SelectList(_context.IncidentReport, "Id", "Id", @case.IncidentReportId);
            return View(@case);
        }

        // GET: Cases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id == null || _context.Case == null)
            {
                return NotFound();
            }

            var @case = await _context.Case
                .Include(w => w.Doctor)
                .Include(w => w.incidentReport)
                .FirstOrDefaultAsync(m => m.CaseId == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Case == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Case'  is null.");
            }
            var @case = await _context.Case.FindAsync(id);
            if (@case != null)
            {
                _context.Case.Remove(@case);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaseExists(int id)
        {
          return (_context.Case?.Any(e => e.CaseId == id)).GetValueOrDefault();
        }
    }
}
