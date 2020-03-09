namespace HappyThoughts.Services.Data.Topics
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Web.ViewModels.InputModels;
    using HappyThoughts.Web.ViewModels.ServiceModels.Topics;
    using HappyThoughts.Web.ViewModels.Topics;

    public interface ITopicsService
    {
        Task CreateAsync(CreateTopicInputModel input);

        Task<T[]> GetAllAsync<T>();

        IQueryable<T> GetAllAsQueryable<T>();

        Task<TopicDetailsViewModel> GetByIdAsViewModelAsync(string id);

        Task IncreaseViews(string id);

        Task DeleteByIdAsync(string topicId);

        Task EditAsync(TopicEditViewModel input);

        Task<TopicServiceModel> GetAllTopicsBySearchAsync(string input, int page = 1);

        TopicServiceModel GetTopicsByCategoryName(string categoryName, int page = 1);

        string GetIdByTitle(string title);

        IEnumerable<TopicInfoViewModel> GetLatestTopics(int page = 1);

        int GetTotalTopicsCount();
    }
}
