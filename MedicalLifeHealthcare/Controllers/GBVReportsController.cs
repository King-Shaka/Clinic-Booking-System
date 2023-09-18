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
    public class GBVReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GBVReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GBVReports
        public async Task<IActionResult> Index()
        {
              return _context.GBVReportTB != null ? 
                          View(await _context.GBVReportTB.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GBVReportTB'  is null.");
        }

        // GET: GBVReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GBVReportTB == null)
            {
                return NotFound();
            }

            var gBVReport = await _context.GBVReportTB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gBVReport == null)
            {
                return NotFound();
            }

            return View(gBVReport);
        }

        // GET: GBVReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GBVReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReportDate,VictimName,PerpetratorName,IncidentDetails")] GBVReport gBVReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gBVReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gBVReport);
        }

        // GET: GBVReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GBVReportTB == null)
            {
                return NotFound();
            }

            var gBVReport = await _context.GBVReportTB.FindAsync(id);
            if (gBVReport == null)
            {
                return NotFound();
            }
            return View(gBVReport);
        }

        // POST: GBVReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReportDate,VictimName,PerpetratorName,IncidentDetails")] GBVReport gBVReport)
        {
            if (id != gBVReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gBVReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GBVReportExists(gBVReport.Id))
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
            return View(gBVReport);
        }

        // GET: GBVReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GBVReportTB == null)
            {
                return NotFound();
            }

            var gBVReport = await _context.GBVReportTB
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gBVReport == null)
            {
                return NotFound();
            }

            return View(gBVReport);
        }

        // POST: GBVReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GBVReportTB == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GBVReportTB'  is null.");
            }
            var gBVReport = await _context.GBVReportTB.FindAsync(id);
            if (gBVReport != null)
            {
                _context.GBVReportTB.Remove(gBVReport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GBVReportExists(int id)
        {
          return (_context.GBVReportTB?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
