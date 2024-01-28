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
    public class Counselling_SessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender emailSender;
        public Counselling_SessionsController(ApplicationDbContext context, IEmailSender _emailSender)
        {
            _context = context;
            emailSender = _emailSender;
        }

        // GET: Counselling_Sessions
        public async Task<IActionResult> My_Sessions()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
         
            ViewBag.Sessions = await _context.Counselling_Sessions.Include(c => c.Appointment).Include(c => c.Counsellor).Include(a => a.Appointment.MainUser).Where(a => a.Appointment.PatientID == user).ToListAsync();
            return View();
        }     
        public async Task<IActionResult> Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.Sessions = await _context.Counselling_Sessions.Include(c => c.Appointment).Include(c => c.Counsellor).Include(a => a.Appointment.MainUser).ToListAsync();
            return View();
        }
    
        public async Task<IActionResult> Report()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Date = DateTime.Now.ToString("dd/MMMM/yyyy");
            ViewBag.Time = DateTime.Now.ToString("HH:MM");
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.Sessions = await _context.Counselling_Sessions.Include(c => c.Appointment).Include(c => c.Counsellor).Include(a => a.Appointment.MainUser).ToListAsync();
            return View();
        }
        public async Task<IActionResult> My_Report()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Date = DateTime.Now.ToString("dd/MMMM/yyyy");
            ViewBag.Time = DateTime.Now.ToString("HH:MM");
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.Sessions = await _context.Counselling_Sessions.Include(c => c.Appointment).Include(c => c.Counsellor).Include(a => a.Appointment.MainUser).Where(a => a.Appointment.MainUser.Id == user).ToListAsync();
            return View();
        }


        // GET: Counselling_Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Counselling_Sessions == null)
            {
                return NotFound();
            }

            var counselling_Sessions = await _context.Counselling_Sessions
                .Include(c => c.Appointment)
                .Include(c => c.Counsellor)
                .FirstOrDefaultAsync(m => m.SessionID == id);
            if (counselling_Sessions == null)
            {
                return NotFound();
            }

            return View(counselling_Sessions);
        }

        // GET: Counselling_Sessions/Create
        public IActionResult Create()
        {
            ViewData["AppointemtID"] = new SelectList(_context.Appointments, "AppointmentId", "Purpose");
            ViewData["CounsellorID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Counselling_Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Counselling_Sessions counselling_Sessions)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            counselling_Sessions.CounsellorID = user;
            try
            {
                _context.Add(counselling_Sessions);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Appointment has been Accepted";
                //Create alert for Patient
                var P = _context.Appointments.Where(a => a.AppointmentID == counselling_Sessions.AppointemtID).FirstOrDefault();
                var alert = new Alert()
                {
                    Message = "Session, dated on " + counselling_Sessions.SessionCreatedDate + " has been Created by the Consellor",
                    IntendedUser = P.PatientID,
                    Purpose = "Notification",

                };
                _context.Alerts.Add(alert);
                await _context.SaveChangesAsync();
                //Create alert for Counsellor
                var alert2 = new Alert()
                {
                    Message = "Session, dated on " + counselling_Sessions.SessionCreatedDate + " has been Created Successfully",
                    IntendedUser = user,
                    Purpose = "Notification",

                };
                _context.Alerts.Add(alert2);
                await _context.SaveChangesAsync();
                //update appointment Status
                P.Status = "Session Created";
                _context.Appointments.Update(P);
                await _context.SaveChangesAsync();
                //Send
                //Find the patient Email

                var Pemail = _context.Users.Where(a => a.Id == P.PatientID).FirstOrDefault();
                var email = User.FindFirstValue(ClaimTypes.Email);
                await emailSender.SendEmailAsync(Pemail.Email, "New  Session",

                    $"<html> <head> <style> body {{ font-family: Arial, sans-serif; }} " +
                    $"h1 {{ color: #336699; }}" +
                    $".cta-button {{ background-color: #008CBA; color: white;" +
                    $" padding: 10px 20px;" +
                    $" text-decoration: none; border-radius: 5px; }}" +
                    $".cta-button:hover {{ background-color: #265580; }}" +
                    $".footer {{ margin-top: 20px; font-size: 12px; color: #888; }}" +
                    $"  </style>" +
                    $"</head>" +
                    $"<body>" +
                    $"" +
                    $"<p>Dear  Patient,</p>" +
                    $"<p>this is a confimation email that New Session  has been Successfully created.</p>" +
                    $"<p>If you have any questions or need assistance, please don't hesitate to contact our support team</p>" +
                    $"<div class='footer'>" +
                    $" <p>Thank you,</p>" +
                    $" <p>Healthcare Team</p>" +
                    $"</div>" +
                    $" </body>" +
                    $"</html>");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
            }
            ViewData["AppointemtID"] = new SelectList(_context.Appointments, "AppointmentId", "Purpose", counselling_Sessions.AppointemtID);
            ViewData["CounsellorID"] = new SelectList(_context.Users, "Id", "Id", counselling_Sessions.CounsellorID);
            return View(counselling_Sessions);
        }
        public async Task<IActionResult> Close_Session(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null || _context.Counselling_Sessions == null)
            {
                return NotFound();
            }

            var counselling_Sessions = await _context.Counselling_Sessions.FindAsync(id);
            if (counselling_Sessions == null)
            {
                return NotFound();
            }
            counselling_Sessions.Status = "Session Closed";
            _context.Update(counselling_Sessions);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Session has been Closed Successfully";
            //Create alert for Patient
            var P = _context.Appointments.Where(a => a.AppointmentID == counselling_Sessions.AppointemtID).FirstOrDefault();
            var alert = new Alert()
            {
                Message = "Counselling Session, dated on " + counselling_Sessions.SessionCreatedDate + " has been Closed by the Consellor",
                IntendedUser = P.PatientID,
                Purpose = "Notification",

            };
            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();
            //Create alert for Counsellor
            var alert2 = new Alert()
            {
                Message = "Session, dated on " + counselling_Sessions.SessionCreatedDate + " has been Closed Successfully",
                IntendedUser = user,
                Purpose = "Notification",

            };
            _context.Alerts.Add(alert2);
            await _context.SaveChangesAsync();
            //update appointment Status
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Open_Session(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null || _context.Counselling_Sessions == null)
            {
                return NotFound();
            }

            var counselling_Sessions = await _context.Counselling_Sessions.FindAsync(id);
            if (counselling_Sessions == null)
            {
                return NotFound();
            }
            counselling_Sessions.Status = "Session Opened";
            _context.Update(counselling_Sessions);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Session has been Opened Successfully";
            //Create alert for Patient
            var P = _context.Appointments.Where(a => a.AppointmentID == counselling_Sessions.AppointemtID).FirstOrDefault();
            var alert = new Alert()
            {
                Message = "Counselling Session, dated on " + counselling_Sessions.SessionCreatedDate + " has been Opened by the Consellor",
                IntendedUser = P.PatientID,
                Purpose = "Notification",

            };
            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();
            //Create alert for Counsellor
            var alert2 = new Alert()
            {
                Message = "Session, dated on " + counselling_Sessions.SessionCreatedDate + " has been Opened Successfully",
                IntendedUser = user,
                Purpose = "Notification",

            };
            _context.Alerts.Add(alert2);
            await _context.SaveChangesAsync();
            //update appointment Status
            return RedirectToAction(nameof(Index));
        }
        // GET: Counselling_Sessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Counselling_Sessions == null)
            {
                return NotFound();
            }

            var counselling_Sessions = await _context.Counselling_Sessions.FindAsync(id);
            if (counselling_Sessions == null)
            {
                return NotFound();
            }
            ViewData["AppointemtID"] = new SelectList(_context.Appointments, "AppointmentId", "Purpose", counselling_Sessions.AppointemtID);
            ViewData["CounsellorID"] = new SelectList(_context.Users, "Id", "Id", counselling_Sessions.CounsellorID);
            return View(counselling_Sessions);
        }

        // POST: Counselling_Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SessionID,SessionCreatedDate,SessionDate,SessionTime,CounsellorID,AppointemtID,Status")] Counselling_Sessions counselling_Sessions)
        {
            if (id != counselling_Sessions.SessionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(counselling_Sessions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Counselling_SessionsExists(counselling_Sessions.SessionID))
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
            ViewData["AppointemtID"] = new SelectList(_context.Appointments, "AppointmentId", "Purpose", counselling_Sessions.AppointemtID);
            ViewData["CounsellorID"] = new SelectList(_context.Users, "Id", "Id", counselling_Sessions.CounsellorID);
            return View(counselling_Sessions);
        }

        // GET: Counselling_Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Counselling_Sessions == null)
            {
                return NotFound();
            }

            var counselling_Sessions = await _context.Counselling_Sessions
                .Include(c => c.Appointment)
                .Include(c => c.Counsellor)
                .FirstOrDefaultAsync(m => m.SessionID == id);
            if (counselling_Sessions != null)
            {
                _context.Counselling_Sessions.Remove(counselling_Sessions);
                await _context.SaveChangesAsync();
            }
            TempData["Success"] = "Session  has been Deleted Successfully";
            //Create alert
            //find the patient
            var P = _context.Appointments.Where(a => a.AppointmentID == counselling_Sessions.AppointemtID).FirstOrDefault();
            var alert = new Alert()
            {
                Message = "Session has be delete, dated on " + counselling_Sessions.SessionCreatedDate + " Contact the Counsellor for more details",
                IntendedUser = P.PatientID,
                Purpose = "Notification",

            };
            _context.Alerts.Add(alert);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Counselling_Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Counselling_Sessions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Counselling_Sessions'  is null.");
            }
            var counselling_Sessions = await _context.Counselling_Sessions.FindAsync(id);
            if (counselling_Sessions != null)
            {
                _context.Counselling_Sessions.Remove(counselling_Sessions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Counselling_SessionsExists(int id)
        {
            return (_context.Counselling_Sessions?.Any(e => e.SessionID == id)).GetValueOrDefault();
        }
    }
}
