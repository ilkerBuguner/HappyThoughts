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

        Task IncreaseViewsAsync(string id);

        Task DeleteByIdAsync(string topicId);

        Task EditAsync(TopicEditViewModel input);

        Task<TopicServiceModel> GetAllTopicsBySearchAsync(string input, int page);

        TopicServiceModel GetTopicsByCategoryName(string categoryName, int page);

        string GetIdByTitle(string title);

        IEnumerable<TopicInfoViewModel> GetLatestTopics(int page = 1);

        TopicServiceModel GetTrendingTopics(int page);

        IEnumerable<TopicInfoViewModel> GetRandomTopics(int page);

        int GetTotalTopicsCount();

        Task LikeTopicAsync(string topicId);
    }
}
