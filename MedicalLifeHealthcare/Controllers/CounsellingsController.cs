using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalLifeHealthcare.Areas.Identity.Data;
using MedicalLifeHealthcare.Models;

namespace MedicalLifeHealthcare.Controllers
{
    public class CounsellingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CounsellingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Counsellings
        public async Task<IActionResult> Index()
        {
              return _context.CounsellingTB != null ? 
                          View(await _context.CounsellingTB.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CounsellingTB'  is null.");
        }

        // GET: Counsellings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CounsellingTB == null)
            {
                return NotFound();
            }

            var counselling = await _context.CounsellingTB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counselling == null)
            {
                return NotFound();
            }

            return View(counselling);
        }

        // GET: Counsellings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Counsellings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SessionDate,TherapistName,PatientName,PatientContact,TherapistContact,SessionNotes,IsCompleted,IsFollowUpRequired,FollowUpDate,FollowUpNotes")] Counselling counselling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(counselling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(counselling);
        }

        // GET: Counsellings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CounsellingTB == null)
            {
                return NotFound();
            }

            var counselling = await _context.CounsellingTB.FindAsync(id);
            if (counselling == null)
            {
                return NotFound();
            }
            return View(counselling);
        }

        // POST: Counsellings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SessionDate,TherapistName,PatientName,PatientContact,TherapistContact,SessionNotes,IsCompleted,IsFollowUpRequired,FollowUpDate,FollowUpNotes")] Counselling counselling)
        {
            if (id != counselling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(counselling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CounsellingExists(counselling.Id))
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
            return View(counselling);
        }

        // GET: Counsellings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CounsellingTB == null)
            {
                return NotFound();
            }

            var counselling = await _context.CounsellingTB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counselling == null)
            {
                return NotFound();
            }

            return View(counselling);
        }

        // POST: Counsellings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CounsellingTB == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CounsellingTB'  is null.");
            }
            var counselling = await _context.CounsellingTB.FindAsync(id);
            if (counselling != null)
            {
                _context.CounsellingTB.Remove(counselling);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CounsellingExists(int id)
        {
          return (_context.CounsellingTB?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
