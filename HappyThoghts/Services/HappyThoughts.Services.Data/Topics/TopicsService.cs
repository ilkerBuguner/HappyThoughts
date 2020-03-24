namespace HappyThoughts.Services.Data.Topics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HappyThoughts.Common;
    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.InputModels;
    using HappyThoughts.Web.ViewModels.ServiceModels.Topics;
    using HappyThoughts.Web.ViewModels.Topics;
    using Microsoft.EntityFrameworkCore;

    public class TopicsService : ITopicsService
    {
        private const string InvalidTopicIdErrorMessage = "Topic with ID: {0} does not exist.";

        private const int TitleMaxLength = 50;

        private const int ContentMinLength = 10;

        private readonly IDeletableEntityRepository<Topic> topicRepository;

        public TopicsService(IDeletableEntityRepository<Topic> topicRepository)
        {
            this.topicRepository = topicRepository;
        }

        public async Task CreateAsync(CreateTopicInputModel input)
        {
            var topic = new Topic
            {
                Title = input.Title,
                Content = input.Content,
                PictureUrl = input.PictureUrl,
                AuthorId = input.AuthorId,
                CategoryId = input.CategoryId,
            };

            await this.topicRepository.AddAsync(topic);
            await this.topicRepository.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string topicId)
        {
            var topic = await this.topicRepository.GetByIdWithDeletedAsync(topicId);

            if (topic == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidTopicIdErrorMessage, topicId));
            }

            this.topicRepository.Delete(topic);
            await this.topicRepository.SaveChangesAsync();
        }

        public async Task EditAsync(TopicEditViewModel input)
        {
            var topicFromDb = await this.topicRepository
                .GetByIdWithDeletedAsync(input.Id);

            if (topicFromDb == null)
            {
                throw new ArgumentNullException(
                     string.Format(InvalidTopicIdErrorMessage, input.Id));
            }

            if (input.Title.Length <= TitleMaxLength && input.Title != null && input.Title != topicFromDb.Title)
            {
                topicFromDb.Title = input.Title;
            }

            if (input.Content != null && input.Content.Length >= ContentMinLength && input.Content != topicFromDb.Content)
            {
                topicFromDb.Content = input.Content;
            }

            this.topicRepository.Update(topicFromDb);
            await this.topicRepository.SaveChangesAsync();
        }

        public async Task<T[]> GetAllAsync<T>()
        {
            return await this.topicRepository
                .All()
                .To<T>()
                .ToArrayAsync();
        }

        public IQueryable<T> GetAllAsQueryable<T>()
        {
            return this.topicRepository
                .All()
                .To<T>();
        }

        public async Task<TopicDetailsViewModel> GetByIdAsViewModelAsync(string id)
        {
            var topic = await this.topicRepository
                .All()
                .Include(x => x.Author)
                .Include(x => x.Category)
                .To<TopicDetailsViewModel>()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (topic == null)
            {
                throw new ArgumentNullException();
            }

            return topic;
        }

        public async Task IncreaseViewsAsync(string id)
        {
            var topic = await this.topicRepository.All().FirstOrDefaultAsync(t => t.Id == id);
            topic.Views += 1;
            await this.topicRepository.SaveChangesAsync();
        }

        public async Task<TopicServiceModel> GetAllTopicsBySearchAsync(string input, int page = GlobalConstants.DefaultPageNumber)
        {
            var searchParts = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var topicsFromDb = await this.GetAllAsync<TopicInfoViewModel>();

            var matchingTopics = new List<TopicInfoViewModel>();

            foreach (var searchPart in searchParts)
            {
                foreach (var topicFromDb in topicsFromDb)
                {
                    var normalizedTitle = topicFromDb.Title.ToLower();
                    var normalizedContent = topicFromDb.Content.ToLower();

                    if (normalizedTitle.Contains(searchPart.ToLower()) || normalizedContent.Contains(searchPart.ToLower()))
                    {
                        matchingTopics.Add(topicFromDb);
                    }
                }
            }

            var topicsForPage = matchingTopics.OrderByDescending(c => c.CreatedOn).Skip((page - 1) * GlobalConstants.DefaultPageSize)
                .Take(GlobalConstants.DefaultPageSize).ToList();

            var serviceModel = new TopicServiceModel()
            {
                TotalTopicsCount = matchingTopics.Count(),
                Topics = topicsForPage,
            };

            return serviceModel;
        }

        public string GetIdByTitle(string title)
        {
            return this.topicRepository.All().FirstOrDefault(t => t.Title == title).Id;
        }

        public IEnumerable<TopicInfoViewModel> GetLatestTopics(int page = GlobalConstants.DefaultPageNumber)
        {
            var topics = this.GetAllAsQueryable<TopicInfoViewModel>()
                .Where(t => t.CreatedOn > DateTime.Now.AddDays(-100))
                .OrderByDescending(t => t.CreatedOn)
                .Skip((page - 1) * GlobalConstants.DefaultPageSize)
                .Take(GlobalConstants.DefaultPageSize)
                .ToList();

            // var topics = topics.ToList().Where(t => t.CreatedOn > DateTime.Now.AddDays(-1)).OrderByDescending(t => t.CreatedOn);            
            return topics;
        }

        public int GetTotalTopicsCount()
        {
            return this.topicRepository.All().Count();
        }

        public TopicServiceModel GetTopicsByCategoryName(string categoryName, int page = GlobalConstants.DefaultPageNumber)
        {
            var topics = this.GetAllAsQueryable<TopicInfoViewModel>()
                .Where(t => t.CategoryName == categoryName)
                .ToList();

            var topicsForPage = topics.OrderByDescending(t => t.CreatedOn)
                .Skip((page - 1) * GlobalConstants.DefaultPageSize)
                .Take(GlobalConstants.DefaultPageSize);

            var serviceModel = new TopicServiceModel()
            {
                TotalTopicsCount = topics.Count,
                Topics = topicsForPage,
            };

            return serviceModel;
        }

        public TopicServiceModel GetTrendingTopics(int page)
        {
            var topics = this.GetAllAsQueryable<TopicInfoViewModel>()
                .Where(t => t.CreatedOn > DateTime.Now.AddDays(-7))
                .OrderByDescending(t => t.Likes)
                .ToList();

            var topicsForPage = topics
                .Skip((page - 1) * GlobalConstants.DefaultPageSize)
                .Take(GlobalConstants.DefaultPageSize);

            var topicsServiceModel = new TopicServiceModel()
            {
                TotalTopicsCount = topics.Count,
                Topics = topicsForPage,
            };

            return topicsServiceModel;
        }

        public IEnumerable<TopicInfoViewModel> GetRandomTopics(int page = GlobalConstants.DefaultPageNumber)
        {
            var topics = this.GetAllAsQueryable<TopicInfoViewModel>()
                .ToList();

            Shuffle(topics);

            topics = topics
                .Skip((page - 1) * GlobalConstants.DefaultPageSize)
                .Take(GlobalConstants.DefaultPageSize)
                .ToList();

            return topics;
        }

        public async Task VoteTopicAsync(string topicId, bool isLike)
        {
            var topicFromDb = await this.topicRepository.GetByIdWithDeletedAsync(topicId);

            if (isLike)
            {
                topicFromDb.Likes++;
            }
            else
            {
                topicFromDb.Dislikes++;
            }

            this.topicRepository.Update(topicFromDb);
            await this.topicRepository.SaveChangesAsync();
        }

        public async Task CancelVoteAsync(string topicId, bool isLike)
        {
            var topicFromDb = await this.topicRepository.GetByIdWithDeletedAsync(topicId);

            if (isLike)
            {
                topicFromDb.Likes--;
            }
            else
            {
                topicFromDb.Dislikes--;
            }

            this.topicRepository.Update(topicFromDb);
            await this.topicRepository.SaveChangesAsync();
        }

        public int GetTopicTotalLikes(string topicId)
        {
            var topicFromDb = this.topicRepository.All().FirstOrDefault(t => t.Id == topicId);

            return topicFromDb.Likes;
        }

        public int GetTopicTotalDislikes(string topicId)
        {
            var topicFromDb = this.topicRepository.All().FirstOrDefault(t => t.Id == topicId);

            return topicFromDb.Dislikes;
        }

        private static void Shuffle(List<TopicInfoViewModel> topics)
        {
            var random = new Random();

            int remainingElementsToShuffle = topics.Count;
            while (remainingElementsToShuffle > 1)
            {
                remainingElementsToShuffle--;
                int randomPos = random.Next(remainingElementsToShuffle + 1);
                var value = topics[randomPos];
                topics[randomPos] = topics[remainingElementsToShuffle];
                topics[remainingElementsToShuffle] = value;
            }
        }
    }
}
