namespace HappyThoughts.Services.Data.Votes
{
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Data.Models.Enumerations;
    using HappyThoughts.Services.Data.Topics;
    using Microsoft.EntityFrameworkCore;

    public class VotesService : IVotesService
    {
        private readonly IRepository<TopicVote> topicVoteRepository;
        private readonly ITopicsService topicsService;

        public VotesService(IRepository<TopicVote> topicVoteRepository, ITopicsService topicsService)
        {
            this.topicVoteRepository = topicVoteRepository;
            this.topicsService = topicsService;
        }

        public async Task<int> VoteTopicAsync(string topicId, string userId, bool isLike)
        {
            var topicVote = await this.topicVoteRepository
                .All()
                .FirstOrDefaultAsync(x => x.TopicId == topicId && x.UserId == userId);

            if (topicVote == null)
            {
                topicVote = new TopicVote()
                {
                    UserId = userId,
                    TopicId = topicId,
                    Type = isLike ? VoteType.Like : VoteType.Dislike,
                };

                if (isLike)
                {
                    await this.topicsService.VoteTopicAsync(topicId, true);
                }
                else
                {
                    await this.topicsService.VoteTopicAsync(topicId, false);
                }

                await this.topicVoteRepository.AddAsync(topicVote);
            }
            else
            {
                await this.ManageVoteStatusAsync(topicId, isLike, topicVote);
            }

            await this.topicVoteRepository.SaveChangesAsync();

            if (isLike)
            {
                return this.topicsService.GetTopicTotalLikes(topicId);
            }
            else
            {
                return this.topicsService.GetTopicTotalDislikes(topicId);
            }
        }

        private async Task ManageVoteStatusAsync(string topicId, bool isLike, TopicVote topicVote)
        {
            if (topicVote.Type == VoteType.Like && isLike == false)
            {
                await this.topicsService.CancelVoteAsync(topicId, true);
                await this.topicsService.VoteTopicAsync(topicId, false);

                topicVote.Type = VoteType.Dislike;
            }
            else if (topicVote.Type == VoteType.Like && isLike == true)
            {
                await this.topicsService.CancelVoteAsync(topicId, true);

                topicVote.Type = VoteType.Neutral;
            }
            else if (topicVote.Type == VoteType.Dislike && isLike == true)
            {
                await this.topicsService.CancelVoteAsync(topicId, false);
                await this.topicsService.VoteTopicAsync(topicId, true);

                topicVote.Type = VoteType.Like;
            }
            else if (topicVote.Type == VoteType.Dislike && isLike == false)
            {
                await this.topicsService.CancelVoteAsync(topicId, false);

                topicVote.Type = VoteType.Neutral;
            }
            else if (topicVote.Type == VoteType.Neutral && isLike == true)
            {
                await this.topicsService.VoteTopicAsync(topicId, true);
                topicVote.Type = VoteType.Like;
            }
            else if (topicVote.Type == VoteType.Neutral && isLike == false)
            {
                await this.topicsService.VoteTopicAsync(topicId, false);
                topicVote.Type = VoteType.Dislike;
            }
        }
    }
}
