using HappyThoughts.Data.Common.Repositories;
using HappyThoughts.Data.Models;
using HappyThoughts.Data.Models.Enumerations;
using HappyThoughts.Services.Data.Topics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HappyThoughts.Services.Data.Votes
{
    public class VotesService : IVotesService
    {
        private readonly IRepository<TopicVote> topicVoteRepository;
        private readonly ITopicsService topicsService;

        public VotesService(IRepository<TopicVote> topicVoteRepository, ITopicsService topicsService)
        {
            this.topicVoteRepository = topicVoteRepository;
            this.topicsService = topicsService;
        }

        public int GetLikes(string topicId)
        {
            throw new System.NotImplementedException();
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
                    await this.topicsService.VoteTopic(topicId, true);
                }
                else
                {
                    await this.topicsService.VoteTopic(topicId, false);
                }

                await this.topicVoteRepository.AddAsync(topicVote);
            }
            else
            {
                if (topicVote.Type == VoteType.Like && isLike == false)
                {
                    await this.topicsService.CancelVote(topicId, true);
                    await this.topicsService.VoteTopic(topicId, false);
                }
                else if (topicVote.Type == VoteType.Dislike && isLike == true)
                {
                    await this.topicsService.CancelVote(topicId, false);
                    await this.topicsService.VoteTopic(topicId, true);
                }

                topicVote.Type = isLike ? VoteType.Like : VoteType.Dislike;
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
    }
}
