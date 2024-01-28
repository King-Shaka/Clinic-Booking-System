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
using MedicalLifeHealthcare.Migrations;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MedicalLifeHealthcare.Controllers
{
    public class TestRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _email;

        public TestRequestsController(ApplicationDbContext context, IEmailSender email)
        {
            _context = context;
            _email = email;
        }

        // GET: TestRequests
        public async Task<IActionResult> Index()
        {
            ViewBag.Patints = (from U in _context.Users
                               join UR in _context.UserRoles on U.Id equals UR.UserId
                               join R in _context.Roles on UR.RoleId equals R.Id
                               where R.Name == "Patient"
                               select U).ToList();
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            ViewBag.Test = _context.TestRequest.Include(t => t.Clinician).Include(t => t.Patient).ToList();
            return View();
        } 
        public async Task<IActionResult> My_Test()
        {
            ViewBag.Patints = (from U in _context.Users
                               join UR in _context.UserRoles on U.Id equals UR.UserId
                               join R in _context.Roles on UR.RoleId equals R.Id
                               where R.Name == "Patient"
                               select U).ToList();
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            ViewBag.Test = _context.TestRequest.Include(t => t.Clinician).Where(a => a.PatientId == user).Include(t => t.Patient).ToList();
            return View();
        }  
        public async Task<IActionResult> All_Test_Requests()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            ViewBag.Test = _context.TestRequest.Include(t => t.Clinician).Include(t => t.Patient).ToList();
            return View();
        }

        // GET: TestRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TestRequest == null)
            {
                return NotFound();
            }

            var testRequest = await _context.TestRequest
                .Include(t => t.Clinician)
                .Include(t => t.Patient)
                .FirstOrDefaultAsync(m => m.TestRequestId == id);
            if (testRequest == null)
            {
                return NotFound();
            }

            return View(testRequest);
        }

        // GET: TestRequests/Create
        public IActionResult Create()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            ViewBag.Patints = (from U in _context.Users
                               join UR in _context.UserRoles on U.Id equals UR.UserId
                               join R in _context.Roles on UR.RoleId equals R.Id
                               where R.Name == "Patient"
                               select U).ToList();
            ViewData["RequestedBy"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PatientId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: TestRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestSampleVM testRequest)
        {
            if (testRequest.PatientId != null && testRequest.TestName != null)
            {
                var test = new TestRequest()
                {
                    PatientId = testRequest.PatientId,
                    RequestedBy = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    TestName = testRequest.TestName,
                    Instructions = testRequest.Instructions,
                };
                _context.TestRequest.Add(test);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Test Request has been Created Successfully";
                TempData["UpdateType"] = "success";
                // alerts for teh person who logged in  
                var alert = new Alert()
                {
                    Message = "Test Request has been Created Successfully dated on" + test.RequestDate,
                    IntendedUser = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Purpose = "Notification"

                };
              
                _context.Alerts.Add(alert);
                await _context.SaveChangesAsync();

                // alerts for the patient
                var Patinet = _context.Users.Where(a => a.Id == testRequest.PatientId).FirstOrDefault();
                var alert2 = new Alert()
                {
                    Message = "Test Request has been Created Successfully dated on" + test.RequestDate,
                    IntendedUser = Patinet.Id,
                    Purpose = "Notification"

                };
                _context.Alerts.Add(alert2);
                await _context.SaveChangesAsync();

                // alerts for all  pathologists
               var  Pathologies = (from U in _context.Users
                                   join UR in _context.UserRoles on U.Id equals UR.UserId
                                   join R in _context.Roles on UR.RoleId equals R.Id
                                   where R.Name == "Pathology"
                                   select U).ToList();
                //loop the list to send the alert to all pathologies 
                foreach (var path in Pathologies)
                {
                    var alert3 = new Alert()
                    {
                        Message = "Test Request has been Created Successfully dated on" + test.RequestDate,
                        IntendedUser = path.Id,
                        Purpose = "Notification"
                    };
                    _context.Alerts.Add(alert3);
                    await _context.SaveChangesAsync();
                }
               
              
                try
                {
                    string supportEmail = "enompilo.healthcare@gmail.com";
                    await _email.SendEmailAsync(Patinet.Email, "Test Requested",
                        $"<html> <head> <style> body {{ font-family: Arial, sans-serif; }} " +
                      $"h1 {{ color: #00347C; }}" +
                      $".cta-button {{ background-color: #00C2CC; color: white;" +
                      $" padding: 10px 20px;" +
                      $" text-decoration: none; border-radius: 5px; }}" +
                      $".cta-button:hover {{ background-color: #00C2CC; }}" +
                      $".footer {{ margin-top: 20px; font-size: 12px; color: #888; }}" +
                      $"  </style>" +
                      $"</head>" +
                      $"<body>" +
                      $"" +
                      $"<h1>Enompilo Health Care!</h1>" +
                      $"<p></p>" +
                      $"<p>This is Notificatio Email About Test Requested on your behalf</p>" +
                      $"<strong><p>Test Request Date and Time: {test.RequestDate}</p></strong>" +
                      $"<p>If you have any questions or need assistance, please don't hesitate to contact our support team at {supportEmail}.</p>" +
                      $"<div class='footer'>" +
                      $" <p>Thank you,</p>" +
                      $" <p>Enompilo health care Team</p>" +
                      $"</div>" +
                      $" </body>" +
                      $"</html>"
                        );
                }
                catch
                {

                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequestedBy"] = new SelectList(_context.Users, "Id", "Id", testRequest.RequestedBy);
            ViewData["PatientId"] = new SelectList(_context.Users, "Id", "Id", testRequest.PatientId);
            return View(testRequest);
        }

        // GET: TestRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TestRequest == null)
            {
                return NotFound();
            }

            var testRequest = await _context.TestRequest.FindAsync(id);
            if (testRequest == null)
            {
                return NotFound();
            }
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var alert = _context.Alerts.Where(a => a.IntendedUser == user).OrderBy(a => a.date).ToList();
            ViewBag.Alert = alert;
            ViewData["RequestedBy"] = new SelectList(_context.Users, "Id", "Id", testRequest.RequestedBy);
            ViewData["PatientId"] = new SelectList(_context.Users, "Id", "Id", testRequest.PatientId);
            return View(testRequest);
        }

        // POST: TestRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestRequestId,PatientId,RequestedBy,RequestDate,TestName,Instructions,Status")] TestRequest testRequest)
        {
            if (id != testRequest.TestRequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestRequestExists(testRequest.TestRequestId))
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
            ViewData["RequestedBy"] = new SelectList(_context.Users, "Id", "Id", testRequest.RequestedBy);
            ViewData["PatientId"] = new SelectList(_context.Users, "Id", "Id", testRequest.PatientId);
            return View(testRequest);
        }

        // GET: TestRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TestRequest == null)
            {
                return NotFound();
            }

            var testRequest = await _context.TestRequest
                .Include(t => t.Clinician)
                .Include(t => t.Patient)
                .FirstOrDefaultAsync(m => m.TestRequestId == id);
            if (testRequest == null)
            {
                return NotFound();
            }

            return View(testRequest);
        }

        // POST: TestRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TestRequest == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TestRequest'  is null.");
            }
            var testRequest = await _context.TestRequest.FindAsync(id);
            if (testRequest != null)
            {
                _context.TestRequest.Remove(testRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestRequestExists(int id)
        {
          return (_context.TestRequest?.Any(e => e.TestRequestId == id)).GetValueOrDefault();
        }
    }
}
