namespace HappyThoughts.Web.Controllers
{
    using System.Threading.Tasks;

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

            return this.View(viewModel);
        }
    }
}
