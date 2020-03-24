namespace HappyThoughts.Services.Data.Votes
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task<int> VoteTopicAsync(string topicId, string userId, bool isLike);

    }
}
