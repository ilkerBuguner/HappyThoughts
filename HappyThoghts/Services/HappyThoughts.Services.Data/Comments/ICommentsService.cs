namespace HappyThoughts.Services.Data.Comments
{
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Web.ViewModels.InputModels.Comments;

    public interface ICommentsService
    {
        Task CreateAsync(CreateCommentInputModel input);

        Task DeleteByIdAsync(string commentId);

        Task<T[]> GetAllAsync<T>();

        IQueryable<T> GetAllAsQueryable<T>();
    }
}
