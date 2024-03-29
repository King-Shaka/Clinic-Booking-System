﻿using System;
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
    public class Medical_FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _email;
        public Medical_FeedbackController(ApplicationDbContext context, IEmailSender email)
        {
            _context = context;
            _email = email;
        }

        // GET: Medical_Feedback
        public async Task<IActionResult> Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.Medicine = _context.Prescription.Where(a => a.PatientId == user).ToList();
            ViewBag.Feed = _context.Medical_Feedback.Include(m => m.Doctor).Include(m => m.Patient).Include(m => m.Prescription).Where(a => a.PatientID == user).ToList();
            return View();
        }
          public async Task<IActionResult> All_feedbacks()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewBag.Medicine = _context.Prescription.Where(a => a.PatientId == user).ToList();
            ViewBag.Feed = _context.Medical_Feedback.Include(m => m.Doctor).Include(m => m.Patient).Include(m => m.Prescription).ToList();
            return View();
        }

        // GET: Medical_Feedback/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medical_Feedback == null)
            {
                return NotFound();
            }

            var medical_Feedback = await _context.Medical_Feedback
                .Include(m => m.Doctor)
                .Include(m => m.Patient)
                .Include(m => m.Prescription)
                .FirstOrDefaultAsync(m => m.FeedbackID == id);
            if (medical_Feedback == null)
            {
                return NotFound();
            }

            return View(medical_Feedback);
        }

        // GET: Medical_Feedback/Create
        public IActionResult Create()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            ViewData["DoctorsID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PrescresptionID"] = new SelectList(_context.Prescription, "Id", "Dosage");
            return View();
        }

        // POST: Medical_Feedback/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medical_Feedback medical_Feedback)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            medical_Feedback.PatientID = user;
            if (ModelState.IsValid)
            {
                _context.Add(medical_Feedback);
                await _context.SaveChangesAsync();
                TempData["Success"] = "New Feeedback Has been Created Successfully";
                TempData["UpdateType"] = "success";
                try
                {
                    var alerts = new Alert()
                    {
                        Message = "Feedback has been Successfully Created",
                        IntendedUser = user,
                        Purpose = "Notification",
                    };
                    _context.Alerts.Add(alerts);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    TempData["Success"] = "New Feedback Has been Created Successfully but email Notification failed";
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorsID"] = new SelectList(_context.Users, "Id", "Id", medical_Feedback.DoctorsID);
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", medical_Feedback.PatientID);
            ViewData["PrescresptionID"] = new SelectList(_context.Prescription, "Id", "Dosage", medical_Feedback.PrescresptionID);
            return View(medical_Feedback);
        }

        // GET: Medical_Feedback/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Alerts = _context.Alerts.Where(a => a.IntendedUser == user).OrderByDescending(a => a.date).ToList();
            if (Alerts.Count > 0)
            {
                ViewBag.Alerts = Alerts;
                TempData["Alerts"] = "Not null";
            }
            if (id == null || _context.Medical_Feedback == null)
            {
                return NotFound();
            }

            var medical_Feedback = await _context.Medical_Feedback.FindAsync(id);
            if (medical_Feedback == null)
            {
                return NotFound();
            }
            ViewData["DoctorsID"] = new SelectList(_context.Users, "Id", "Id", medical_Feedback.DoctorsID);
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", medical_Feedback.PatientID);
            ViewData["PrescresptionID"] = new SelectList(_context.Prescription, "Id", "Dosage", medical_Feedback.PrescresptionID);
            return View(medical_Feedback);
        }

        // POST: Medical_Feedback/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( Medical_Feedback medical_Feedback)
        {
            if (medical_Feedback.DoctorsFeedback != null)
            {
                try
                {
                    var feed = _context.Medical_Feedback.Where(a => a.FeedbackID == medical_Feedback.FeedbackID).FirstOrDefault();
                    if(feed != null)
                    {
                        feed.AnsweredDate = DateTime.Now;
                        feed.DoctorsFeedback = medical_Feedback.DoctorsFeedback;
                        feed.DoctorsID = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    }
                    _context.Update(feed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Medical_FeedbackExists(medical_Feedback.FeedbackID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Success"] = " Feeedback Has been Provided Successfully";
                TempData["UpdateType"] = "success";
                return RedirectToAction(nameof(All_feedbacks));
            }
            ViewData["DoctorsID"] = new SelectList(_context.Users, "Id", "Id", medical_Feedback.DoctorsID);
            ViewData["PatientID"] = new SelectList(_context.Users, "Id", "Id", medical_Feedback.PatientID);
            ViewData["PrescresptionID"] = new SelectList(_context.Prescription, "Id", "Dosage", medical_Feedback.PrescresptionID);
            return View(medical_Feedback);
        }

        // GET: Medical_Feedback/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medical_Feedback == null)
            {
                return NotFound();
            }

            var medical_Feedback = await _context.Medical_Feedback
                .Include(m => m.Doctor)
                .Include(m => m.Patient)
                .Include(m => m.Prescription)
                .FirstOrDefaultAsync(m => m.FeedbackID == id);
            if (medical_Feedback == null)
            {
                return NotFound();
            }
            if (medical_Feedback != null)
            {
                _context.Medical_Feedback.Remove(medical_Feedback);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = " Feeedback Has been Deleted Successfully";
            TempData["UpdateType"] = "success";
            return RedirectToAction(nameof(Index));
        }

        // POST: Medical_Feedback/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medical_Feedback == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Medical_Feedback'  is null.");
            }
            var medical_Feedback = await _context.Medical_Feedback.FindAsync(id);
            if (medical_Feedback != null)
            {
                _context.Medical_Feedback.Remove(medical_Feedback);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Medical_FeedbackExists(int id)
        {
            return (_context.Medical_Feedback?.Any(e => e.FeedbackID == id)).GetValueOrDefault();
        }
    }
}
