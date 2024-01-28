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

namespace MedicalLifeHealthcare.Controllers
{
    public class QuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ques
        public async Task<IActionResult> MyQues()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            var applicationDbContext = _context.Que.Include(q => q.Appointments).Include(q => q.Clinician).Include(a => a.Appointments).Where(a => a.Appointments.PatientID == user);
            return View(await applicationDbContext.ToListAsync());
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
            var applicationDbContext = _context.Que.Include(q => q.Appointments).Include(q => q.Clinician);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id == null || _context.Que == null)
            {
                return NotFound();
            }

            var que = await _context.Que
                .Include(q => q.Appointments)
                .Include(q => q.Clinician)
                .FirstOrDefaultAsync(m => m.QueID == id);
            if (que == null)
            {
                return NotFound();
            }

            return View(que);
        }

        // GET: Ques/Create
        public IActionResult Create()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewData["AppointmentID"] = new SelectList(_context.Appointments, "AppointmentID", "Reason");
            ViewData["ClinicianID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Ques/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment_QueVM que)
        {
            var Que = new Que()
            {
                AppointmentID = que.AppointmentID,
                Time = que.Time,
                RoomNumber = que.RoomNumber,
                ClinicianID = que.ClinicianID,
            };
            if (que.RoomNumber != null && que.ClinicianID != null)
            {
                _context.Que.Add(Que);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentID"] = new SelectList(_context.Appointments, "AppointmentID", "Reason", que.AppointmentID);
            ViewData["ClinicianID"] = new SelectList(_context.Users, "Id", "Id", que.ClinicianID);
            return View(que);
        }

        // GET: Ques/Edit/5
        public async Task<IActionResult> Done(int? id)
        {
            if (id == null || _context.Que == null)
            {
                return NotFound();
            }

            var que = await _context.Que.FindAsync(id);
            if (que == null)
            {
                return NotFound();
            }
            que.Status = "Done";
            _context.Que.Update(que);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Appointment has been Done and Closed";
            TempData["UpdateType"] = "success";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Notify(int? id)
        {
            if (id == null || _context.Que == null)
            {
                return NotFound();
            }

            var que = await _context.Que.FindAsync(id);
            if (que == null)
            {
                return NotFound();
            }
            que.Status = "Called In";
            _context.Que.Update(que);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Appointment has been called in and Patient Notified";
            TempData["UpdateType"] = "success";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Que == null)
            {
                return NotFound();
            }

            var que = await _context.Que.FindAsync(id);
            if (que == null)
            {
                return NotFound();
            }
            ViewData["AppointmentID"] = new SelectList(_context.Appointments, "AppointmentID", "Reason", que.AppointmentID);
            ViewData["ClinicianID"] = new SelectList(_context.Users, "Id", "Id", que.ClinicianID);
            return View(que);
        }

        // POST: Ques/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QueID,AppointmentID,dateOFQue,Time,RoomNumber,ClinicianID,Status")] Que que)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id != que.QueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(que);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QueExists(que.QueID))
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
            ViewData["AppointmentID"] = new SelectList(_context.Appointments, "AppointmentID", "Reason", que.AppointmentID);
            ViewData["ClinicianID"] = new SelectList(_context.Users, "Id", "Id", que.ClinicianID);
            return View(que);
        }

        // GET: Ques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Que == null)
            {
                return NotFound();
            }

            var que = await _context.Que
                .Include(q => q.Appointments)
                .Include(q => q.Clinician)
                .FirstOrDefaultAsync(m => m.QueID == id);
            if (que == null)
            {
                return NotFound();
            }

            return View(que);
        }

        // POST: Ques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Que == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Que'  is null.");
            }
            var que = await _context.Que.FindAsync(id);
            if (que != null)
            {
                _context.Que.Remove(que);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QueExists(int id)
        {
            return (_context.Que?.Any(e => e.QueID == id)).GetValueOrDefault();
        }
    }
}
