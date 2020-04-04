namespace HappyThoughts.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HappyThoughts.Web.ViewModels.Messages;
    using HappyThoughts.Web.ViewModels.Users;

    public interface IUsersService
    {
        Task<ApplicationUserDetailsViewModel> GetUserAsViewModelByIdAsync(string id);

        Task<int> GetUsersCountAsync();

        string GetUsernameById(string id);

        Task BanAsync(string userId);

        Task UnbanAsync(string userId);

        Task PromoteAsync(string userId);

        Task Demote(string userId);

        Task<bool> IsPromotedAsync(string userId);

        Task<bool> IsBannedAsync(string userId);

        Task<bool> IsAdminAsync(string userId);
    }
}
