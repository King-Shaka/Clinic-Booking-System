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
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MedicalLifeHealthcare.Controllers
{
    public class SampleResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SampleResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SampleResults
        public async Task<IActionResult> My_Results(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            if (id == null)
            {
                var applicationDbContext = _context.SampleResults.Include(s => s.Pathology).Include(s => s.Samples).Include(s => s.Samples.TestRequest).Where(a => a.Samples.TestRequest.PatientId == user);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.SampleResults.Include(s => s.Pathology).Include(s => s.Samples).Where(a =>( a.Samples.TestRequestId == id && a.Samples.TestRequest.PatientId == user)).Include(s => s.Samples.TestRequest);
                return View(await applicationDbContext.ToListAsync());
            }
          
           
        }  public async Task<IActionResult> Index(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            if (id == null)
            {
                var applicationDbContext = _context.SampleResults.Include(s => s.Pathology).Include(s => s.Samples).Include(s => s.Samples.TestRequest);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.SampleResults.Include(s => s.Pathology).Include(s => s.Samples).Where(a => a.SamplesID == id).Include(s => s.Samples.TestRequest);
                return View(await applicationDbContext.ToListAsync());
            }
          
           
        }

        public async Task<IActionResult> Report(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            if (id == null)
            {
                var applicationDbContext = _context.SampleResults.Include(s => s.Pathology).Include(s => s.Samples).Include(s => s.Samples.TestRequest);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.SampleResults.Include(s => s.Pathology).Include(s => s.Samples).Where(a => a.SamplesID == id).Include(s => s.Samples.TestRequest);
                return View(await applicationDbContext.ToListAsync());
            }


        }

        // GET: SampleResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SampleResults == null)
            {
                return NotFound();
            }

            var sampleResults = await _context.SampleResults
                .Include(s => s.Pathology)
                .Include(s => s.Samples)
                .FirstOrDefaultAsync(m => m.SampleResultsId == id);
            if (sampleResults == null)
            {
                return NotFound();
            }

            return View(sampleResults);
        }

        // GET: SampleResults/Create
        public IActionResult Create()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            ViewData["PathologyID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["SamplesID"] = new SelectList(_context.Samples, "SampleCollectionId", "SampleCollectionId");
            return View();
        }

        // POST: SampleResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SampleResults sampleResults)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            sampleResults.PathologyID = user;
            if (ModelState.IsValid)
            {
                _context.Add(sampleResults);
                await _context.SaveChangesAsync();

                var sample = _context.Samples.Where(a => a.SampleCollectionId == sampleResults.SamplesID).FirstOrDefault();
                if(sample != null)
                {
                    var test = _context.TestRequest.Where(a => a.TestRequestId == sample.TestRequestId).FirstOrDefault();
                    test.Status = "Test Done";
                    _context.TestRequest.Update(test);
                    await _context.SaveChangesAsync();
                    //Alert
                    var alert1 = new Alert()
                    {
                        IntendedUser = test.PatientId,
                     
                        Message = "Test Sample has result are ready",
                        Purpose = "Notification",
                    };
                    _context.Alerts.Add(alert1);
                    await _context.SaveChangesAsync();
                    //Alert2
                    var alert2 = new Alert()
                    {
                        IntendedUser = test.RequestedBy,
                        Message = "Your Test Sample Result has been Done",
                        Purpose = "Notification",
                    };
                    _context.Alerts.Add(alert2);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PathologyID"] = new SelectList(_context.Users, "Id", "Id", sampleResults.PathologyID);
            ViewData["SamplesID"] = new SelectList(_context.Samples, "SampleCollectionId", "SampleCollectionId", sampleResults.SamplesID);
            return View(sampleResults);
        }

        // GET: SampleResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SampleResults == null)
            {
                return NotFound();
            }

            var sampleResults = await _context.SampleResults.FindAsync(id);
            if (sampleResults == null)
            {
                return NotFound();
            }
            ViewData["PathologyID"] = new SelectList(_context.Users, "Id", "Id", sampleResults.PathologyID);
            ViewData["SamplesID"] = new SelectList(_context.Samples, "SampleCollectionId", "SampleCollectionId", sampleResults.SamplesID);
            return View(sampleResults);
        }

        // POST: SampleResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SampleResultsId,SamplesID,ResultDate,TestName,ResultValue,ReferenceRange,UnitOfMeasure,Interpretation,Comments,PathologyID")] SampleResults sampleResults)
        {
            if (id != sampleResults.SampleResultsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sampleResults);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SampleResultsExists(sampleResults.SampleResultsId))
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
            ViewData["PathologyID"] = new SelectList(_context.Users, "Id", "Id", sampleResults.PathologyID);
            ViewData["SamplesID"] = new SelectList(_context.Samples, "SampleCollectionId", "SampleCollectionId", sampleResults.SamplesID);
            return View(sampleResults);
        }

        // GET: SampleResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SampleResults == null)
            {
                return NotFound();
            }

            var sampleResults = await _context.SampleResults
                .Include(s => s.Pathology)
                .Include(s => s.Samples)
                .FirstOrDefaultAsync(m => m.SampleResultsId == id);
            if (sampleResults == null)
            {
                return NotFound();
            }

            return View(sampleResults);
        }

        // POST: SampleResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SampleResults == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SampleResults'  is null.");
            }
            var sampleResults = await _context.SampleResults.FindAsync(id);
            if (sampleResults != null)
            {
                _context.SampleResults.Remove(sampleResults);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SampleResultsExists(int id)
        {
          return (_context.SampleResults?.Any(e => e.SampleResultsId == id)).GetValueOrDefault();
        }
    }
}
