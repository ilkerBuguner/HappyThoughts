namespace HappyThoughts.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HappyThoughts.Services.Data.Replies;
    using HappyThoughts.Web.ViewModels.InputModels.Replies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class RepliesController : BaseController
    {
        private readonly IRepliesService repliesService;

        public RepliesController(IRepliesService repliesService)
        {
            this.repliesService = repliesService;
        }

        [Authorize]
        public async Task<IActionResult> Create(CreateReplyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Topics/Details?topicId={input.TopicId}");
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            input.AuthorId = userId;

            await this.repliesService.CreateAsync(input);

            return this.Redirect($"/Topics/Details?topicId={input.TopicId}");
        }
    }
}
