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
    public class Medical_RecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Medical_RecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Medical_Records
        public async Task<IActionResult> MyRecords()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            var USER = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Medical_Records.Include(m => m.Nurse).Include(a => a.File).Where(a => a.File.PatientID == USER);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Index(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            var applicationDbContext = _context.Medical_Records.Include(m => m.Nurse).Where(a => a.FileID == id);
            ViewBag.ID = id;
            return View(await applicationDbContext.ToListAsync());



        }
        public async Task<IActionResult> All_Records(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            var applicationDbContext = _context.Medical_Records.Include(m => m.Nurse);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Medical_Records/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id == null || _context.Medical_Records == null)
            {
                return NotFound();
            }

            var medical_Records = await _context.Medical_Records
                .Include(m => m.Nurse)
                .FirstOrDefaultAsync(m => m.RecordsID == id);
            if (medical_Records == null)
            {
                return NotFound();
            }

            return View(medical_Records);
        }

        // GET: Medical_Records/Create
        public IActionResult Create(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            var file = _context.Medical_File.Where(a => a.FileID == id).Include(m => m.mainUser).ToList();
            ViewBag.File = file;
            ViewBag.ID = id;
            return View();
        }

        // POST: Medical_Records/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medical_Records medical_Records)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            medical_Records.NurseID = user;
            if (ModelState.IsValid)
            {
                _context.Add(medical_Records);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = medical_Records.FileID });
            }
            ViewData["NurseID"] = new SelectList(_context.Users, "Id", "Id", medical_Records.NurseID);
            return View(medical_Records);
        }

        // GET: Medical_Records/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id == null || _context.Medical_Records == null)
            {
                return NotFound();
            }

            var medical_Records = await _context.Medical_Records.FindAsync(id);
            if (medical_Records == null)
            {
                return NotFound();
            }
            ViewData["NurseID"] = new SelectList(_context.Users, "Id", "Id", medical_Records.NurseID);
            return View(medical_Records);
        }

        // POST: Medical_Records/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecordsID,CurrentMedications,BloodPressure,HeartRate,Temperature,Height,Weight,Notes,RecordDate,NurseID")] Medical_Records medical_Records)
        {
            if (id != medical_Records.RecordsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medical_Records);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Medical_RecordsExists(medical_Records.RecordsID))
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
            ViewData["NurseID"] = new SelectList(_context.Users, "Id", "Id", medical_Records.NurseID);
            return View(medical_Records);
        }

        // GET: Medical_Records/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || _context.Medical_Records == null)
            {
                return NotFound();
            }

            var medical_Records = await _context.Medical_Records
                .Include(m => m.Nurse)
                .FirstOrDefaultAsync(m => m.RecordsID == id);
            if (medical_Records == null)
            {
                return NotFound();
            }

            return View(medical_Records);
        }

        // POST: Medical_Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medical_Records == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Medical_Records'  is null.");
            }
            var medical_Records = await _context.Medical_Records.FindAsync(id);
            if (medical_Records != null)
            {
                _context.Medical_Records.Remove(medical_Records);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Medical_RecordsExists(int id)
        {
            return (_context.Medical_Records?.Any(e => e.RecordsID == id)).GetValueOrDefault();
        }
    }
}
