using MedicalLifeHealthcare.Areas.Identity.Data;

namespace MedicalLifeHealthcare.Core.Repositories
{
    public interface IUserRepository
    {
        ICollection<ApplicationUser> GetUsers();

        ApplicationUser GetUser(string id);

        ApplicationUser UpdateUser(ApplicationUser user);
    }
}
