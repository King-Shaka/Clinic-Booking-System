using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MedicalLifeHealthcare.Areas.Identity.Data;
using MedicalLifeHealthcare.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using NuGet.Versioning;

namespace MedicalLifeHealthcare.Controllers
{
    public class Session_FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Session_FeedbackController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Session_Feedback
        public async Task<IActionResult> My_Feedback()
        {
            var applicationDbContext = _context.Session_Feedback.Include(s => s.Session);
            return View(await applicationDbContext.ToListAsync());
        }  
        public async Task<IActionResult> Index(int? ID)
        {
            if(ID != null)
            {
                ViewBag.Sessions = _context.Session_Feedback.Include(s => s.Session).Where(a => a.SessionID == ID).ToList();
            }
            else
            {
                ViewBag.Sessions = _context.Session_Feedback.Include(s => s.Session).ToList();
            }
      
            return View();
        }

        // GET: Session_Feedback/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Session_Feedback == null)
            {
                return NotFound();
            }

            var session_Feedback = await _context.Session_Feedback
                .Include(s => s.Session)
                .FirstOrDefaultAsync(m => m.FeedbackID == id);
            if (session_Feedback == null)
            {
                return NotFound();
            }

            return View(session_Feedback);
        }

        // GET: Session_Feedback/Create
        public IActionResult Create(int? id)
        {

            ViewData["SessionID"] = new SelectList(_context.Counselling_Sessions, "SessionID", "SessionID");
            return View();
        }

        // POST: Session_Feedback/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Session_Feedback session_Feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(session_Feedback);
                await _context.SaveChangesAsync();
                var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
                var Role = await _userManager.GetRolesAsync(user);
                
                if (Role.Contains("Counsellor"))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(My_Feedback));
                }
              
            }
            ViewData["SessionID"] = new SelectList(_context.Counselling_Sessions, "SessionID", "SessionID", session_Feedback.SessionID);
            return View(session_Feedback);
        }

        // GET: Session_Feedback/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Session_Feedback == null)
            {
                return NotFound();
            }

            var session_Feedback = await _context.Session_Feedback.FindAsync(id);
            if (session_Feedback == null)
            {
                return NotFound();
            }
            ViewData["SessionID"] = new SelectList(_context.Counselling_Sessions, "SessionID", "SessionID", session_Feedback.SessionID);
            return View(session_Feedback);
        }

        // POST: Session_Feedback/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Session_Feedback session_Feedback)
        {
            
            if (session_Feedback.FeedbackID != 0)
            {
                try
                {
                    var feedback = _context.Session_Feedback.Where(a => a.FeedbackID == session_Feedback.FeedbackID).Include(a => a.Session.Appointment.MainUser).FirstOrDefault();
                    feedback.Counsellor_Feedback = session_Feedback.Counsellor_Feedback;
                    feedback.Satus = "Replied";
                    _context.Update(feedback);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Feedback asnwer has been successful";
                    TempData["UpdateType"] = "success";
                    var alert = new Alert()
                    {
                        IntendedUser = feedback.Session.Appointment.MainUser.Email,
                        Message = "Feedback  has been answeres successful by the counselor"
                    };
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Session_FeedbackExists(session_Feedback.FeedbackID))
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
            ViewData["SessionID"] = new SelectList(_context.Counselling_Sessions, "SessionID", "SessionID", session_Feedback.SessionID);
            return View(session_Feedback);
        }

        // GET: Session_Feedback/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Session_Feedback == null)
            {
                return NotFound();
            }

            var session_Feedback = await _context.Session_Feedback
                .Include(s => s.Session)
                .FirstOrDefaultAsync(m => m.FeedbackID == id);
            if (session_Feedback == null)
            {
                return NotFound();
            }

            return View(session_Feedback);
        }

        // POST: Session_Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Session_Feedback == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Session_Feedback'  is null.");
            }
            var session_Feedback = await _context.Session_Feedback.FindAsync(id);
            if (session_Feedback != null)
            {
                _context.Session_Feedback.Remove(session_Feedback);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Session_FeedbackExists(int id)
        {
          return (_context.Session_Feedback?.Any(e => e.FeedbackID == id)).GetValueOrDefault();
        }
    }
}
