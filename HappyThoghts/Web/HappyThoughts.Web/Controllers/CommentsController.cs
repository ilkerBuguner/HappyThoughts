namespace HappyThoughts.Web.Controllers
{
    using System.Threading.Tasks;

    using HappyThoughts.Services.Data.Comments;
    using HappyThoughts.Web.ViewModels.InputModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            this.commentsService = commentsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Topics/Details?topicId={input.TopicId}");
            }

            await this.commentsService.CreateAsync(input);

            return this.Redirect($"/Topics/Details?topicId={input.TopicId}");
        }
    }
}
