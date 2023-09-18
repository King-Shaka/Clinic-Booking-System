using MedicalLifeHealthcare.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalLifeHealthcare.Core.ViewModels
{
    public class EditUserViewModel
    {
        public ApplicationUser User { get; set; }

        public IList<SelectListItem> Roles { get; set; }
    }
}
