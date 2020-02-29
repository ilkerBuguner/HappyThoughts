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

        public async Task<T[]> GetAllAsync<T>()
        {
            return await this.topicRepository
                .AllAsNoTracking()
                .To<T>()
                .ToArrayAsync();
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
    }
}
