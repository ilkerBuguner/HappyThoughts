namespace HappyThoughts.Services.Data.Forums
{
    using System.Threading.Tasks;

    using HappyThoughts.Web.ViewModels.Forums;

    public interface IForumsService
    {
        Task<ForumStatsViewModel> GetForumStatsAsync();
    }
}
