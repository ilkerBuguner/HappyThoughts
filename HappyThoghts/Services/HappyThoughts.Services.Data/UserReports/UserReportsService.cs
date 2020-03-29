namespace HappyThoughts.Services.Data.UserReports
{
    using System;
    using System.Threading.Tasks;

    using HappyThoughts.Data.Common.Repositories;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.InputModels.UserReports;
    using Microsoft.EntityFrameworkCore;

    public class UserReportsService : IUserReportsService
    {
        private const string InvalidUserReportIdErrorMessage = "UserReport with ID: {0} does not exist.";


        private readonly IDeletableEntityRepository<UserReport> userReportRepository;

        public UserReportsService(IDeletableEntityRepository<UserReport> userReportRepository)
        {
            this.userReportRepository = userReportRepository;
        }

        public async Task SendAsync(CreateUserReportInputModel input)
        {
            var userReport = new UserReport()
            {
                Title = input.Title,
                Description = input.Description,
                SenderId = input.SenderId,
                ReportedUserId = input.ReportedUserId,
            };

            await this.userReportRepository.AddAsync(userReport);
            await this.userReportRepository.SaveChangesAsync();
        }

        public async Task<T[]> GetAllAsync<T>()
        {
            return await this.userReportRepository
                .All()
                .To<T>()
                .ToArrayAsync();
        }

        public async Task DeleteByIdAsync(string id)
        {
            var userReportFromDb = await this.userReportRepository
                .GetByIdWithDeletedAsync(id);

            if (userReportFromDb == null)
            {
                throw new ArgumentException(
                    string.Format(InvalidUserReportIdErrorMessage, id));
            }

            this.userReportRepository.Delete(userReportFromDb);
            await this.userReportRepository.SaveChangesAsync();
        }
    }
}
