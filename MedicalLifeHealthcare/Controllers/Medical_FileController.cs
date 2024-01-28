using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalLifeHealthcare.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.Services;
using MedicalLifeHealthcare.Areas.Identity.Data;

namespace MedicalLifeHealthcare.Controllers
{
    public class Medical_FileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        public Medical_FileController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: Medical_File
        public async Task<IActionResult> Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            var applicationDbContext = _context.Medical_File.Include(m => m.mainUser);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> My_Medical_File()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            var applicationDbContext = _context.Medical_File.Where(a => a.PatientID == user).Include(m => m.mainUser).FirstOrDefault();
			var file = _context.Medical_File.Where(a => a.PatientID == user).Include(m => m.mainUser).ToList();
            if(applicationDbContext != null)
            {
                ViewBag.File = applicationDbContext.FileID;
                TempData["file"] = "found";
            }
            else
            {
                TempData["file"] = null;
            }
            if(file != null)
            {
                ViewBag.MyFile = file;
                TempData["file"] = "found";
            }
            else
            {
                TempData["file"] = null;
            }
           
  

			return View();

        }
        [HttpGet]
        public async Task<IActionResult> Patient_File(int? ID)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            var applicationDbContext = _context.Medical_File.Where(a => a.FileID == ID).Include(m => m.mainUser).FirstOrDefault();
			var file = _context.Medical_File.Where(a => a.FileID == ID).Include(m => m.mainUser).ToList();
			
            ViewBag.File = applicationDbContext.FileID;
            ViewBag.MyFile = file;

			return View();
        }

        // GET: Medical_File/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id == null || _context.Medical_File == null)
            {
                return NotFound();
            }

            var medical_File = await _context.Medical_File
                .Include(m => m.mainUser)
                .FirstOrDefaultAsync(m => m.FileID == id);
            if (medical_File == null)
            {
                return NotFound();
            }

            return View(medical_File);
        }

        // GET: Medical_File/Create
        public IActionResult Create()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
		public IActionResult Create_File()
		{
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id");
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create_File( Medical_File medical_File)
		{
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            medical_File.PatientID = user;
            if (ModelState.IsValid)
			{
				_context.Add(medical_File);
				await _context.SaveChangesAsync();
                try
                {
                    var alerts = new Alert()
                    {
                        Message = "New Medical File has been Created.",
                        Purpose = "Notify",
                        IntendedUser = user,
                    };
                    _context.Alerts.Add(alerts);
                    await _context.SaveChangesAsync();
                    try
                    {
                        string supportEmail = "enompilo.healthcare@gmail.com";
                        var logUser = _context.Users.Where(a => a.Id == user).FirstOrDefault();
                        await _emailSender.SendEmailAsync(logUser.Email, "Medical File Created", $"<html> <head> <style> body {{ font-family: Arial, sans-serif; }} " +
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
                          $"<p>This is Notificatio Email About Medical</p>" +
                          $"<p>Please Note the Medical File has been Created</p>." +

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
                }
                catch
                {
                }
                TempData["Success"] = "Medical File  has been Created Successfully";
                TempData["UpdateType"] = "success";
                return RedirectToAction("Index","Enompilo");

			}
            TempData["Success"] = "Someting Went wrong, Please make sure to fill all the required fields with valid informtion";
            TempData["UpdateType"] = "danger";
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", medical_File.PatientID);
            //return RedirectToAction("Patient", "Walk_In", medical_File);
            return View(medical_File);

        }

		// POST: Medical_File/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FileID,PatientID,Gender,BirthDate,IDNumber,AddressLine1,Province,Country,PostalCode,EmergencyPerson,EmergencyContactNo,Relationship,BloodType,Allergies,AnySurgeries,ExtraNotes")] Medical_File medical_File)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medical_File);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", medical_File.PatientID);
            return View(medical_File);
        }

        // GET: Medical_File/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id == null || _context.Medical_File == null)
            {
                return NotFound();
            }

            var medical_File = await _context.Medical_File.FindAsync(id);
            if (medical_File == null)
            {
                return NotFound();
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", medical_File.PatientID);
            return View(medical_File);
        }

        // POST: Medical_File/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FileID,PatientID,Gender,BirthDate,IDNumber,AddressLine1,Province,Country,PostalCode,EmergencyPerson,EmergencyContactNo,Relationship,BloodType,Allergies,AnySurgeries,ExtraNotes")] Medical_File medical_File)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != medical_File.FileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    medical_File.PatientID = user;
                    _context.Update(medical_File);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Medical_FileExists(medical_File.FileID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (User.IsInRole("Patient"))
                {
                    return RedirectToAction(nameof(My_Medical_File));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
               
            }
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", medical_File.PatientID);
            return View(medical_File);
        }

        // GET: Medical_File/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medical_File == null)
            {
                return NotFound();
            }

            var medical_File = await _context.Medical_File
                .Include(m => m.mainUser)
                .FirstOrDefaultAsync(m => m.FileID == id);
            if (medical_File == null)
            {
                return NotFound();
            }

            return View(medical_File);
        }

        // POST: Medical_File/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medical_File == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Medical_File'  is null.");
            }
            var medical_File = await _context.Medical_File.FindAsync(id);
            if (medical_File != null)
            {
                _context.Medical_File.Remove(medical_File);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Medical_FileExists(int id)
        {
          return (_context.Medical_File?.Any(e => e.FileID == id)).GetValueOrDefault();
        }
    }
}
