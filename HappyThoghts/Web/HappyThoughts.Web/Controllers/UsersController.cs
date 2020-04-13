namespace HappyThoughts.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HappyThoughts.Common;
    using HappyThoughts.Services.Data.Users;
    using HappyThoughts.Services.Data.UsersFollowers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;
        private readonly IUsersFollowersService usersFollowersService;

        public UsersController(IUsersService usersService, IUsersFollowersService usersFollowersService)
        {
            this.usersService = usersService;
            this.usersFollowersService = usersFollowersService;
        }

        public async Task<IActionResult> Profile(string id)
        {
            var viewModel = await this.usersService.GetUserAsViewModelByIdAsync(id);

            viewModel.FollowersOfUser = this.usersService.GetCurrentUsersFollowers(id);
            viewModel.FollowedUsers = this.usersService.GetCurrentUsersFollowingUsers(id);

            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isFollowing = this.usersFollowersService.IsFollowing(currentUserId, id);
            var isUserBanned = await this.usersService.IsBannedAsync(id);
            var isUserModerator = await this.usersService.IsPromotedAsync(id);
            var isUserAdmin = await this.usersService.IsAdminAsync(id);

            if (isUserBanned)
            {
                viewModel.IsBanned = true;
            }

            if (isUserAdmin)
            {
                viewModel.IsAdmin = true;
            }

            if (isUserModerator)
            {
                viewModel.IsModerator = true;
            }

            if (isFollowing)
            {
                viewModel.IsFollowing = true;
            }

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Follow(string followedUserId)
        {
            string followingUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.usersFollowersService.FollowAsync(followingUserId, followedUserId);

            this.TempData["SuccessInfo"] = "You successfully followed this user!";

            return this.RedirectToAction(nameof(this.Profile), new { id = followedUserId });
        }

        public async Task<IActionResult> Unfollow(string unfollowedUserId)
        {
            string unfollowingUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            await this.usersFollowersService.UnfollowAsync(unfollowingUserId, unfollowedUserId);

            this.TempData["SuccessInfo"] = "You successfully unfollowed this user!";

            return this.RedirectToAction(nameof(this.Profile), new { id = unfollowedUserId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Promote(string userId)
        {
            var isUserAlreadyPromoted = await this.usersService.IsPromotedAsync(userId);
            if (isUserAlreadyPromoted)
            {
                this.TempData["UnsuccessInfo"] = "This user is already a Moderator!";
            }
            else
            {
                await this.usersService.PromoteAsync(userId);
                this.TempData["SuccessInfo"] = "The user is a Moderator now!";
            }

            return this.RedirectToAction(nameof(this.Profile), new { id = userId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Demote(string userId)
        {
            var isUserPromoted = await this.usersService.IsPromotedAsync(userId);
            if (isUserPromoted)
            {
                await this.usersService.Demote(userId);
                this.TempData["SuccessInfo"] = "The user is not a Moderator now!";
            }
            else
            {
                this.TempData["UnsuccessInfo"] = "The user cannot be demoted because he is not in a role above user!";
            }

            return this.RedirectToAction(nameof(this.Profile), new { id = userId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        public async Task<IActionResult> Ban(string userId)
        {
            var isBannedUserAdmin = await this.usersService.IsAdminAsync(userId);
            if (!isBannedUserAdmin)
            {
                await this.usersService.BanAsync(userId);

                this.TempData["SuccessInfo"] = "The user is banned successfully!";
            }
            else
            {
                this.TempData["UnsuccessInfo"] = "The user cannot be banned because he is Admin!";
            }

            return this.RedirectToAction(nameof(this.Profile), new { id = userId });
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
        public async Task<IActionResult> Unban(string userId)
        {
            await this.usersService.UnbanAsync(userId);

            this.TempData["SuccessInfo"] = "The user is unbanned successfully!";

            return this.RedirectToAction(nameof(this.Profile), new { id = userId });
        }
    }
}
