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
    public class TestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tests
        public async Task<IActionResult> Index()
        {
              return _context.TestTB != null ? 
                          View(await _context.TestTB.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TestTB'  is null.");
        }

        // GET: Tests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TestTB == null)
            {
                return NotFound();
            }

            var tests = await _context.TestTB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tests == null)
            {
                return NotFound();
            }

            return View(tests);
        }

        // GET: Tests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TestName,TestDate,PatientId")] Tests tests)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tests);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tests);
        }

        // GET: Tests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TestTB == null)
            {
                return NotFound();
            }

            var tests = await _context.TestTB.FindAsync(id);
            if (tests == null)
            {
                return NotFound();
            }
            return View(tests);
        }

        // POST: Tests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TestName,TestDate,PatientId")] Tests tests)
        {
            if (id != tests.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestsExists(tests.Id))
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
            return View(tests);
        }

        // GET: Tests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TestTB == null)
            {
                return NotFound();
            }

            var tests = await _context.TestTB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tests == null)
            {
                return NotFound();
            }

            return View(tests);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TestTB == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TestTB'  is null.");
            }
            var tests = await _context.TestTB.FindAsync(id);
            if (tests != null)
            {
                _context.TestTB.Remove(tests);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestsExists(int id)
        {
          return (_context.TestTB?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
