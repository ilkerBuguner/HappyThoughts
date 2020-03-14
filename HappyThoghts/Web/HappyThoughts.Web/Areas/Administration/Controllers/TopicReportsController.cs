namespace HappyThoughts.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HappyThoughts.Common;
    using HappyThoughts.Services.Data.TopicReports;
    using HappyThoughts.Web.Controllers;
    using HappyThoughts.Web.ViewModels.TopicReports;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class TopicReportsController : BaseController
    {
        private readonly ITopicReportsService topicReportsService;

        public TopicReportsController(ITopicReportsService topicReportsService)
        {
            this.topicReportsService = topicReportsService;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var topicReportsViewModel = (await this.topicReportsService.GetAllAsync<TopicReportDetailsViewModel>()).ToList();
            var topicReports = topicReportsViewModel.OrderByDescending(tr => tr.SendOn);

            return this.View(topicReports);
        }

        [Authorize]
        public async Task<IActionResult> Delete(string topicReportId)
        {
            await this.topicReportsService.DeleteByIdAsync(topicReportId);

            return this.Redirect("/Administration/TopicReports/All");
        }
    }
}
