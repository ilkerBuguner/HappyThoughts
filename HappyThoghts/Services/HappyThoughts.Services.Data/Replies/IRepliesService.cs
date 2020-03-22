namespace HappyThoughts.Services.Data.Replies
{
    using System.Threading.Tasks;

    using HappyThoughts.Web.ViewModels.InputModels.Replies;

    public interface IRepliesService
    {
        Task CreateAsync(CreateReplyInputModel input);

        Task EditAsync(string replyId, string replyContent);

        Task DeleteByIdAsync(string replyId);
    }
}
