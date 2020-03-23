using System.Threading.Tasks;

namespace HappyThoughts.Services.Data.Votes
{
    public interface IVotesService
    {
        Task<int> VoteTopicAsync(string topicId, string userId, bool isLike);

        int GetLikes(string topicId);
    }
}
