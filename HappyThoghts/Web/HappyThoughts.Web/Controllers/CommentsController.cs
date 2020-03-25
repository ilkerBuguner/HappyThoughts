namespace HappyThoughts.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HappyThoughts.Common;
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Topics/Details?topicId={input.TopicId}");
            }

            await this.commentsService.EditAsync(input.Id, input.CommentContent);

            return this.Redirect($"/Topics/Details?topicId={input.TopicId}");
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id, string authorId, string topicId, string topicAuthorId)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (this.User.IsInRole(GlobalConstants.AdministratorRoleName) || currentUserId == authorId || currentUserId == topicAuthorId)
            {
                await this.commentsService.DeleteByIdAsync(id);
            }

            return this.Redirect($"/Topics/Details?topicId={topicId}");
        }
    }
}
