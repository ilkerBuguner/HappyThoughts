namespace HappyThoughts.Services.Data.TopicReports
{
    using System;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.InputModels.TopicReports;
    using Microsoft.EntityFrameworkCore;

    public class TopicReportsService : ITopicReportsService
    {
        private const string InvalidTopicReportIdErrorMessage = "TopicReport with ID: {0} does not exist.";

        private readonly IDeletableEntityRepository<TopicReport> topicReportRepository;

        public TopicReportsService(IDeletableEntityRepository<TopicReport> topicReportRepository)
        {
            this.topicReportRepository = topicReportRepository;
        }

        public async Task SendAsync(CreateTopicReportInputModel input)
        {
            var topicReport = new TopicReport()
            {
                Title = input.Title,
                Description = input.Description,
                SenderId = input.SenderId,
                TopicId = input.TopicId,
            };

            await this.topicReportRepository.AddAsync(topicReport);
            await this.topicReportRepository.SaveChangesAsync();
        }

        public async Task<T[]> GetAllAsync<T>()
        {
            return await this.topicReportRepository
                .All()
                .To<T>()
                .ToArrayAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var topicReportFromDb = await this.topicReportRepository
                .GetByIdWithDeletedAsync(id);

            if (topicReportFromDb == null)
            {
                throw new ArgumentException(
                    string.Format(InvalidTopicReportIdErrorMessage, id));
            }

            this.topicReportRepository.Delete(topicReportFromDb);
            await this.topicReportRepository.SaveChangesAsync();
        }
    }
}
