namespace HappyThoughts.Services.Data.TopicReports
{
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Web.ViewModels.InputModels.TopicReports;

    public class TopicReportsService : ITopicReportsService
    {
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
    }
}
