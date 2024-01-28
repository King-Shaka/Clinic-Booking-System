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
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MedicalLifeHealthcare.Controllers
{
    public class SamplesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public SamplesController(ApplicationDbContext context,IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
    }

        // GET: Samples
        public async Task<IActionResult> Index(int? Id)
        {
            if(Id == null)
            {
                var applicationDbContext = _context.Samples.Include(s => s.TestRequest).Include(a => a.TestRequest.Patient).Include(a => a.TestRequest.Clinician);
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _context.Samples.Include(s => s.TestRequest).Include(a => a.TestRequest.Patient).Include(a => a.TestRequest.Clinician).Where(a => a.TestRequestId == Id);
                return View(await applicationDbContext.ToListAsync());
            }
          
           
        }
        public async Task<IActionResult> All_Samples(int? Id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            if (Id == null)
            {
                ViewBag.Test = _context.Samples.Include(s => s.TestRequest).Include(a => a.TestRequest.Patient).Include(a => a.TestRequest.Clinician).ToList();
                return View();
            }
            else
            {
               ViewBag.Test = _context.Samples.Include(s => s.TestRequest).Include(a => a.TestRequest.Patient).Include(a => a.TestRequest.Clinician).Where(a => a.TestRequestId == Id).ToList();
                return View();
            }


        }

        // GET: Samples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Samples == null)
            {
                return NotFound();
            }

            var samples = await _context.Samples
                .Include(s => s.TestRequest)
                .FirstOrDefaultAsync(m => m.SampleCollectionId == id);
            if (samples == null)
            {
                return NotFound();
            }

            return View(samples);
        }

        // GET: Samples/Create
        public IActionResult Create()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            ViewData["TestRequestId"] = new SelectList(_context.TestRequest, "TestRequestId", "TestRequestId");
            return View();
        }

        // POST: Samples/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestSampleVM samples)
        {
            if (samples.CollectionMethod != null && samples.CollectionLocation != null && samples.SampleContainerNumber != null)
            {
                var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var sam = new Samples()
                {
                    TestRequestId = samples.TestRequestId,
                    CollectorName = user,
                    CollectionLocation = samples.CollectionLocation,
                    CollectionMethod = samples.CollectionMethod,
                    SampleContainerNumber = samples.SampleContainerNumber
                };
                _context.Add(sam);
                await _context.SaveChangesAsync();
                TempData["Success"] = "sample has been registered Successfully";
                TempData["UpdateType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            ViewData["TestRequestId"] = new SelectList(_context.TestRequest, "TestRequestId", "TestRequestId", samples.TestRequestId);
            return View(samples);
        }

        // GET: Samples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Samples == null)
            {
                return NotFound();
            }

            var samples = await _context.Samples.FindAsync(id);
            if (samples == null)
            {
                return NotFound();
            }
            ViewData["TestRequestId"] = new SelectList(_context.TestRequest, "TestRequestId", "TestRequestId", samples.TestRequestId);
            return View(samples);
        } 
        public async Task<IActionResult> Process(int? id)
        {
            if (id == null || _context.Samples == null)
            {
                return NotFound();
            }

            var samples = await _context.Samples.FindAsync(id);
            if (samples == null)
            {
                return NotFound();
            }
            var test = _context.TestRequest.Where(a => a.TestRequestId == samples.TestRequestId).FirstOrDefault();
            if (test != null)
            {
                test.Status = "In Process";
                _context.TestRequest.Update(test);
                await _context.SaveChangesAsync();
                //Alert
                var alert1 = new Alert() {
                    IntendedUser = test.PatientId,
                    Message = "Your Tets Sample has been Process",
                    Purpose = "Notification",
                };
                _context.Alerts.Add(alert1);
                await _context.SaveChangesAsync();
                //Alert2
                var alert2 = new Alert()
                {
                    IntendedUser = test.RequestedBy,
                    Message = "Test Sample has been Process",
                    Purpose = "Notification",
                };
                _context.Alerts.Add(alert2);
                await _context.SaveChangesAsync();

                //Email
                var patient = _context.Users.Where(a => a.Id == test.PatientId).FirstOrDefault();
                await _emailSender.SendEmailAsync(patient.Email, "Test Samples", "Your test samples has been Process in the labs");
                TempData["Success"] = "sample has been changed to Process mode Successfully";
                TempData["UpdateType"] = "success";
                return RedirectToAction(nameof(All_Samples));
            }
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            return View(samples);
        }   public async Task<IActionResult> Collected(int? id)
        {
            if (id == null || _context.Samples == null)
            {
                return NotFound();
            }

            var samples = await _context.Samples.FindAsync(id);
            if (samples == null)
            {
                return NotFound();
            }
            var test = _context.TestRequest.Where(a => a.TestRequestId == samples.TestRequestId).FirstOrDefault();
            if (test != null)
            {
                test.Status = "Collected";
                _context.TestRequest.Update(test);
                await _context.SaveChangesAsync();
                //Alert
                var alert1 = new Alert() {
                    IntendedUser = test.PatientId,
                    Message = "Your Tets Sample has been Collected",
                    Purpose = "Notification",
                };
                _context.Alerts.Add(alert1);
                await _context.SaveChangesAsync();
                //Alert2
                var alert2 = new Alert()
                {
                    IntendedUser = test.RequestedBy,
                    Message = "Test Sample has been Collected",
                    Purpose = "Notification",
                };
                _context.Alerts.Add(alert2);
                await _context.SaveChangesAsync();

                //Email
                var patient = _context.Users.Where(a => a.Id == test.PatientId).FirstOrDefault();
                await _emailSender.SendEmailAsync(patient.Email, "Test Samples", "Your test samples has been collected to the labs");
                TempData["Success"] = "sample has been Collected Successfully";
                TempData["UpdateType"] = "success";
                return RedirectToAction(nameof(All_Samples));
            }
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            return View(samples);
        }

        // POST: Samples/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SampleCollectionId,TestRequestId,CollectionDate,CollectorName,CollectionLocation,CollectionMethod,SampleContainerNumber")] Samples samples)
        {
            if (id != samples.SampleCollectionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(samples);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SamplesExists(samples.SampleCollectionId))
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
            ViewData["TestRequestId"] = new SelectList(_context.TestRequest, "TestRequestId", "TestRequestId", samples.TestRequestId);
            return View(samples);
        }

        // GET: Samples/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Samples == null)
            {
                return NotFound();
            }

            var samples = await _context.Samples.FindAsync(id);
            if (samples != null)
            {
                _context.Samples.Remove(samples);
                await _context.SaveChangesAsync();
                TempData["Success"] = "sample has been Deleted Successfully";
                TempData["UpdateType"] = "success";
                return RedirectToAction(nameof(Index));
            }

            return View(samples);
        }

        // POST: Samples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Samples == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Samples'  is null.");
            }
            var samples = await _context.Samples.FindAsync(id);
            if (samples != null)
            {
                _context.Samples.Remove(samples);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SamplesExists(int id)
        {
          return (_context.Samples?.Any(e => e.SampleCollectionId == id)).GetValueOrDefault();
        }
    }
}
