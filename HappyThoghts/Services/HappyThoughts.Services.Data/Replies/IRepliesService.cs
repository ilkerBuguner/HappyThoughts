namespace HappyThoughts.Services.Data.Replies
{
    using System.Threading.Tasks;

    using HappyThoughts.Web.ViewModels.InputModels.Replies;

    public interface IRepliesService
    {
        Task CreateAsync(CreateReplyInputModel input);
    }
}
