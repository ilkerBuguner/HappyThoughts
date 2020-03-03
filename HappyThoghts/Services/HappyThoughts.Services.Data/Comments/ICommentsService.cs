using HappyThoughts.Web.ViewModels.InputModels.Comments;
using System.Threading.Tasks;

namespace HappyThoughts.Services.Data.Comments
{
    public interface ICommentsService
    {
        Task CreateAsync(CreateCommentInputModel input);

        Task<T[]> GetAllAsync<T>();
    }
}
