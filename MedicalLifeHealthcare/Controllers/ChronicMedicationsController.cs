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
    public class ChronicMedicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChronicMedicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChronicMedications
        public async Task<IActionResult> Index()
        {
              return _context.ChronicMedicationTB != null ? 
                          View(await _context.ChronicMedicationTB.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ChronicMedicationTB'  is null.");
        }

        // GET: ChronicMedications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChronicMedicationTB == null)
            {
                return NotFound();
            }

            var chronicMedication = await _context.ChronicMedicationTB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chronicMedication == null)
            {
                return NotFound();
            }

            return View(chronicMedication);
        }

        // GET: ChronicMedications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChronicMedications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MedicationName,Dosage,PatientId")] ChronicMedication chronicMedication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chronicMedication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chronicMedication);
        }

        // GET: ChronicMedications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChronicMedicationTB == null)
            {
                return NotFound();
            }

            var chronicMedication = await _context.ChronicMedicationTB.FindAsync(id);
            if (chronicMedication == null)
            {
                return NotFound();
            }
            return View(chronicMedication);
        }

        // POST: ChronicMedications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedicationName,Dosage,PatientId")] ChronicMedication chronicMedication)
        {
            if (id != chronicMedication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chronicMedication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChronicMedicationExists(chronicMedication.Id))
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
            return View(chronicMedication);
        }

        // GET: ChronicMedications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChronicMedicationTB == null)
            {
                return NotFound();
            }

            var chronicMedication = await _context.ChronicMedicationTB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chronicMedication == null)
            {
                return NotFound();
            }

            return View(chronicMedication);
        }

        // POST: ChronicMedications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChronicMedicationTB == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ChronicMedicationTB'  is null.");
            }
            var chronicMedication = await _context.ChronicMedicationTB.FindAsync(id);
            if (chronicMedication != null)
            {
                _context.ChronicMedicationTB.Remove(chronicMedication);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChronicMedicationExists(int id)
        {
          return (_context.ChronicMedicationTB?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
