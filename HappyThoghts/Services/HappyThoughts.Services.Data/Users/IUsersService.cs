namespace HappyThoughts.Services.Data.Users
{
    using System.Threading.Tasks;

    using HappyThoughts.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task<ApplicationUserDetailsViewModel> GetUserAsViewModelByIdAsync(string id);

        Task<int> GetUsersCount();

        string GetUsernameById(string id);
    }
}
