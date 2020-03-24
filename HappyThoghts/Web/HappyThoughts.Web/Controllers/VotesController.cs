namespace HappyThoughts.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HappyThoughts.Services.Data.Votes;
    using HappyThoughts.Web.ViewModels.InputModels.Votes;
    using HappyThoughts.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TopicVoteResponseModel>> TopicVote(TopicVoteInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var totalLikesOrDislikes = await this.votesService.VoteTopicAsync(input.TopicId, userId, input.IsLike);
            var responseModel = new TopicVoteResponseModel();

            if (input.IsLike)
            {
                responseModel.LikesCount = totalLikesOrDislikes;
            }
            else
            {
                responseModel.DislikesCount = totalLikesOrDislikes;
            }

            return responseModel;
        }
    }
}
