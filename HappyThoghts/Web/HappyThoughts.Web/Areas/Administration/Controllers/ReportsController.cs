namespace HappyThoughts.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Common;
    using HappyThoughts.Services.Data.TopicReports;
    using HappyThoughts.Services.Data.UserReports;
    using HappyThoughts.Web.Controllers;
    using HappyThoughts.Web.ViewModels.Reports;
    using HappyThoughts.Web.ViewModels.TopicReports;
    using HappyThoughts.Web.ViewModels.UserReports;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.ModeratorRoleName)]
    [Area("Administration")]
    public class ReportsController : BaseController
    {
        private readonly ITopicReportsService topicReportsService;
        private readonly IUserReportsService userReportsService;

        public ReportsController(ITopicReportsService topicReportsService, IUserReportsService userReportsService)
        {
            this.topicReportsService = topicReportsService;
            this.userReportsService = userReportsService;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var topicReportsViewModel = (await this.topicReportsService.GetAllAsync<TopicReportDetailsViewModel>()).ToList();
            var topicReports = topicReportsViewModel.OrderByDescending(tr => tr.SendOn);

            var userReportsViewModel = (await this.userReportsService.GetAllAsync<UserReportDetailsViewModel>()).ToList();
            var userReports = userReportsViewModel.OrderByDescending(ur => ur.SendOn);

            var viewModel = new ReportsListingViewModel()
            {
                TopicReports = topicReports,
                UserReports = userReports,
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string topicReportId, string userReportId)
        {
            if (topicReportId != null)
            {
                await this.topicReportsService.DeleteByIdAsync(topicReportId);
            }
            else if (userReportId != null)
            {
                await this.userReportsService.DeleteByIdAsync(userReportId);
            }

            return this.Redirect("/Administration/Reports/All");
        }
    }
}
