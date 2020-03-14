namespace HappyThoughts.Services.Data.TopicReports
{
    using System.Threading.Tasks;

    using HappyThoughts.Web.ViewModels.InputModels.TopicReports;

    public interface ITopicReportsService
    {
        Task SendAsync(CreateTopicReportInputModel input);

        Task<T[]> GetAllAsync<T>();

        Task DeleteByIdAsync(string id);
    }
}
