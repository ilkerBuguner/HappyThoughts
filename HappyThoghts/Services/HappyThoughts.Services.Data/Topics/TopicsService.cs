namespace HappyThoughts.Services.Data.Topics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.InputModels;
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

        public async Task IncreaseViews(string id)
        {
            var topic = await this.topicRepository.GetByIdWithDeletedAsync(id);
            topic.Views += 1;
            await this.topicRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TopicInfoViewModel>> GetAllTopicsBySearchAsync(string input)
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

            return matchingTopics;
        }
    }
}
