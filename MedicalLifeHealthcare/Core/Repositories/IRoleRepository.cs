using MedicalLifeHealthcare.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace MedicalLifeHealthcare.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
