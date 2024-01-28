using MedicalLifeHealthcare.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MedicalLifeHealthcare.Areas.Identity.Data;
using MedicalLifeHealthcare.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using MedicalLifeHealthcare.Core.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalLifeHealthcare.Controllers
{
    public class RoleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public RoleController(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }
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

        [Authorize(Policy = Constants.Policies.RequirePathology)]
        public IActionResult Pathology()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireWalkins)]
        public IActionResult Walkins()
        {
            return View();
        }

        //[Authorize(Policy = "RequireAdmin")]
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Doctor},{Constants.Roles.Nurse},{Constants.Roles.Counsellor},{Constants.Roles.Pathology},{Constants.Roles.Walkins}")]
        public IActionResult Admin()
        {
            return View();
        }


        public IActionResult Patients()
        {
            var users = _unitOfWork.User.GetUsers();
            return View(users);
        }
        public async Task<IActionResult> EditPatients(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();

            var vm = new EditUserViewModel
            {
                User = user,
                Roles = roleItems
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.User.GetUser(data.User.Id);
            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            //Loop through the roles in ViewModel
            //Check if the Role is Assigned In DB
            //If Assigned -> Do Nothing
            //If Not Assigned -> Add Role

            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();

            foreach (var role in data.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        rolesToDelete.Add(role.Text);
                    }
                }
            }

            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToDelete.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToDelete);
            }

            user.FirstName = data.User.FirstName;
            user.LastName = data.User.LastName;
            user.Email = data.User.Email;

            _unitOfWork.User.UpdateUser(user);

            return RedirectToAction("Edit", new { id = user.Id });
        }
    }
}
