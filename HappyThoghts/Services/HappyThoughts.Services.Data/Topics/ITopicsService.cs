namespace HappyThoughts.Services.Data.Topics
{
    using System.Threading.Tasks;

    using HappyThoughts.Web.ViewModels.InputModels;
    using HappyThoughts.Web.ViewModels.Topics;

    public interface ITopicsService
    {
        Task CreateAsync(CreateTopicInputModel input);

        Task<T[]> GetAllAsync<T>();

        Task<TopicDetailsViewModel> GetByIdAsViewModelAsync(string id);

        Task IncreaseViews(string id);

        Task DeleteByIdAsync(string topicId);

        Task EditAsync(TopicEditViewModel input);
    }
}
