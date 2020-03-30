namespace HappyThoughts.Web.Controllers
{
    using System.Threading.Tasks;
    using HappyThoughts.Common;
    using HappyThoughts.Services.Data.Users;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> Profile(string id)
        {
            var viewModel = await this.usersService.GetUserAsViewModelByIdAsync(id);

            var isUserBanned = await this.usersService.IsBannedAsync(id);
            var isUserAdmin = await this.usersService.IsAdminAsync(id);

            if (isUserBanned)
            {
                viewModel.IsBanned = true;
            }

            if (isUserAdmin)
            {
                viewModel.IsAdmin = true;
            }

            return this.View(viewModel);
        }

        public async Task<IActionResult> Ban(string userId)
        {
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName) || this.User.IsInRole(GlobalConstants.ModeratorRoleName))
            {
                var isBannedUserAdmin = await this.usersService.IsAdminAsync(userId);
                if (!isBannedUserAdmin)
                {
                    await this.usersService.BanAsync(userId);

                    this.TempData["BanInfo"] = "The user is banned successfully!";
                }
                else
                {
                    this.TempData["CannotBanInfo"] = "The user cannot be banned because he is Admin!";
                }

            }

            return this.RedirectToAction(nameof(this.Profile), new { id = userId });
        }

        public async Task<IActionResult> Unban(string userId)
        {
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName) || this.User.IsInRole(GlobalConstants.ModeratorRoleName))
            {
                await this.usersService.UnbanAsync(userId);

                this.TempData["BanInfo"] = "The user is unbanned successfully!";
            }

            return this.RedirectToAction(nameof(this.Profile), new { id = userId });
        }
    }
}
