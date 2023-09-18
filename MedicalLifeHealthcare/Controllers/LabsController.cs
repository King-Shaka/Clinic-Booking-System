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
    public class LabsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LabsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Labs
        public async Task<IActionResult> Index()
        {
              return _context.LabTB != null ? 
                          View(await _context.LabTB.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.LabTB'  is null.");
        }

        // GET: Labs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LabTB == null)
            {
                return NotFound();
            }

            var labs = await _context.LabTB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labs == null)
            {
                return NotFound();
            }

            return View(labs);
        }

        // GET: Labs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Labs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TestId,ResultDetails")] Labs labs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labs);
        }

        // GET: Labs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LabTB == null)
            {
                return NotFound();
            }

            var labs = await _context.LabTB.FindAsync(id);
            if (labs == null)
            {
                return NotFound();
            }
            return View(labs);
        }

        // POST: Labs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TestId,ResultDetails")] Labs labs)
        {
            if (id != labs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabsExists(labs.Id))
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
            return View(labs);
        }

        // GET: Labs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LabTB == null)
            {
                return NotFound();
            }

            var labs = await _context.LabTB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labs == null)
            {
                return NotFound();
            }

            return View(labs);
        }

        // POST: Labs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LabTB == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LabTB'  is null.");
            }
            var labs = await _context.LabTB.FindAsync(id);
            if (labs != null)
            {
                _context.LabTB.Remove(labs);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabsExists(int id)
        {
          return (_context.LabTB?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
