namespace HappyThoughts.Services.Data.UserReports
{
    using System.Threading.Tasks;

    using HappyThoughts.Web.ViewModels.InputModels.UserReports;

    public interface IUserReportsService
    {
        Task SendAsync(CreateUserReportInputModel input);

        Task<T[]> GetAllAsync<T>();

        Task DeleteByIdAsync(string id);
    }
}
