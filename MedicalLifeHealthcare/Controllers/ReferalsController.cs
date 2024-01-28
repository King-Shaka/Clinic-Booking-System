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
    public class ReferalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReferalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Referals
        public async Task<IActionResult> Counselling_Referalls(int? Id)
        {
            if(Id == null)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
                if (Alerts.Count > 0)
                {
                    ViewBag.Alerts = Alerts;
                    TempData["Alerts"] = "Not null";
                }
                var applicationDbContext = _context.Referal.Include(r => r.CaseInfor).Include(r => r.Doctor).Include(a => a.CaseInfor.incidentReport.Patient).Where(a => a.ReferallType == "Counselling");
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
                if (Alerts.Count > 0)
                {
                    ViewBag.Alerts = Alerts;
                    TempData["Alerts"] = "Not null";
                }
                var applicationDbContext = _context.Referal.Include(r => r.CaseInfor).Include(r => r.Doctor).Include(a => a.CaseInfor.incidentReport.Patient).Where(a => (a.CaseID == Id && a.ReferallType == "Counselling"));
                return View(await applicationDbContext.ToListAsync());
            }
         
        }    public async Task<IActionResult> Index(int? Id)
        {
            if(Id == null)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
                if (Alerts.Count > 0)
                {
                    ViewBag.Alerts = Alerts;
                    TempData["Alerts"] = "Not null";
                }
                var applicationDbContext = _context.Referal.Include(r => r.CaseInfor).Include(r => r.Doctor).Include(a => a.CaseInfor.incidentReport.Patient);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
                if (Alerts.Count > 0)
                {
                    ViewBag.Alerts = Alerts;
                    TempData["Alerts"] = "Not null";
                }
                var applicationDbContext = _context.Referal.Include(r => r.CaseInfor).Include(r => r.Doctor).Include(a => a.CaseInfor.incidentReport.Patient).Where(a => a.CaseID == Id);
                return View(await applicationDbContext.ToListAsync());
            }
         
        }  
        public async Task<IActionResult> My_referals()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            ViewBag.Date = DateTime.Now.ToString("dd/MMMM/yyyy");
            ViewBag.Time = DateTime.Now.ToString("HH:MM");
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            var applicationDbContext = _context.Referal.Include(r => r.CaseInfor).Include(r => r.Doctor).Include(a => a.CaseInfor.incidentReport.Patient).Where( a => a.CaseInfor.incidentReport.PatientID == user);
            return View(await applicationDbContext.ToListAsync());
        }  public async Task<IActionResult> My_referal()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            var applicationDbContext = _context.Referal.Include(r => r.CaseInfor).Include(r => r.Doctor).Include(a => a.CaseInfor.incidentReport.Patient).Where( a => a.CaseInfor.incidentReport.PatientID == user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Referals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id == null || _context.Referal == null)
            {
                return NotFound();
            }

            var referal = await _context.Referal
                .Include(r => r.CaseInfor)
                .Include(r => r.Doctor)
                .FirstOrDefaultAsync(m => m.ReferalId == id);
            if (referal == null)
            {
                return NotFound();
            }

            return View(referal);
        }

        // GET: Referals/Create
        public IActionResult Create()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewData["CaseID"] = new SelectList(_context.Case, "CaseId", "CaseId");
            ViewData["DoctorID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Referals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Referal referal)
        {
            if (ModelState.IsValid)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                referal.DoctorID = user;
                _context.Add(referal);
                await _context.SaveChangesAsync();
                var casse = _context.Case.Where(a => a.CaseId == referal.CaseID).Include(a => a.incidentReport).FirstOrDefault();
                TempData["Success"] = "GBV Case  Has been Referre  Successfully";
                TempData["UpdateType"] = "success";
                var alerts = new Alert()
                {
                    Message = "Your GBV case Has been Referre for "+referal.ReferallType+" by our doctor Successfully",
                    IntendedUser = casse.incidentReport.PatientID,
                    Purpose = "Notification",
                };
                _context.Alerts.Add(alerts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CaseID"] = new SelectList(_context.Case, "CaseId", "CaseId", referal.CaseID);
            ViewData["DoctorID"] = new SelectList(_context.Users, "Id", "Id", referal.DoctorID);
            return View(referal);
        }

        // GET: Referals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id == null || _context.Referal == null)
            {
                return NotFound();
            }

            var referal = await _context.Referal.FindAsync(id);
            if (referal == null)
            {
                return NotFound();
            }
            ViewData["CaseID"] = new SelectList(_context.Case, "CaseId", "CaseId", referal.CaseID);
            ViewData["DoctorID"] = new SelectList(_context.Users, "Id", "Id", referal.DoctorID);
            return View(referal);
        }

        // POST: Referals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReferalId,CaseID,ReferallType,Notes,DoctorID")] Referal referal)
        {
            if (id != referal.ReferalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferalExists(referal.ReferalId))
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
            ViewData["CaseID"] = new SelectList(_context.Case, "CaseId", "CaseId", referal.CaseID);
            ViewData["DoctorID"] = new SelectList(_context.Users, "Id", "Id", referal.DoctorID);
            return View(referal);
        }

        // GET: Referals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id == null || _context.Referal == null)
            {
                return NotFound();
            }

            var referal = await _context.Referal
                .Include(r => r.CaseInfor)
                .Include(r => r.Doctor)
                .FirstOrDefaultAsync(m => m.ReferalId == id);
            if (referal == null)
            {
                return NotFound();
            }

            return View(referal);
        }

        // POST: Referals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Referal == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Referal'  is null.");
            }
            var referal = await _context.Referal.FindAsync(id);
            if (referal != null)
            {
                _context.Referal.Remove(referal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferalExists(int id)
        {
          return (_context.Referal?.Any(e => e.ReferalId == id)).GetValueOrDefault();
        }
    }
}
