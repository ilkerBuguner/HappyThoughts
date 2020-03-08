using HappyThoughts.Web.ViewModels.InputModels.TopicReports;
using System.Threading.Tasks;

namespace HappyThoughts.Services.Data.TopicReports
{
    public interface ITopicReportsService
    {
        Task SendAsync(CreateTopicReportInputModel input);
    }
}
