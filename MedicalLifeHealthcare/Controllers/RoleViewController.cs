using MedicalLifeHealthcare.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalLifeHealthcare.Controllers
{
    public class RoleViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireDoctor)]
        public IActionResult Doctor()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireNurse)]
        public IActionResult Nurse()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireCounsellor)]
        public IActionResult Counsellor()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireWalkins)]
        public IActionResult Walkinsr()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequirePathology)]
        public IActionResult Pathology()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdmin")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
