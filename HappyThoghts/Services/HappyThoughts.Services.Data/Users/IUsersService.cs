namespace HappyThoughts.Services.Data.Users
{
    using HappyThoughts.Web.ViewModels.Users;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        Task<ApplicationUserDetailsViewModel> GetUserAsViewModelByIdAsync(string id);
    }
}
