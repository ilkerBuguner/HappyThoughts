namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HappyThoughts.Services.Data.TopicReports;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Services.Data.UserReports;
    using HappyThoughts.Services.Data.Users;
    using HappyThoughts.Web.ViewModels.InputModels.TopicReports;
    using HappyThoughts.Web.ViewModels.InputModels.UserReports;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ReportsController : BaseController
    {
        private readonly ITopicsService topicsService;
        private readonly ITopicReportsService topicReportsService;
        private readonly IUsersService usersService;
        private readonly IUserReportsService userReportsService;

        public ReportsController(
            ITopicsService topicsService,
            ITopicReportsService topicReportsService,
            IUsersService usersService,
            IUserReportsService userReportsService)
        {
            this.topicsService = topicsService;
            this.topicReportsService = topicReportsService;
            this.usersService = usersService;
            this.userReportsService = userReportsService;
        }

        [Authorize]
        public async Task<IActionResult> ReportTopic(string topicId)
        {
            var topic = await this.topicsService.GetByIdAsViewModelAsync(topicId);
            this.ViewData["TopicId"] = topic.Id;
            this.ViewData["TopicTitle"] = topic.Title;

            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReportTopic(CreateTopicReportInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Reports/ReportTopic?topicId={input.TopicId}");
            }

            var senderId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.SenderId = senderId;

            await this.topicReportsService.SendAsync(input);

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult ReportUser(string userId)
        {
            var userUsername = this.usersService.GetUsernameById(userId);
            this.ViewData["UserId"] = userId;
            this.ViewData["Username"] = userUsername;

            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReportUser(CreateUserReportInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Reports/Send?userId={input.ReportedUserId}");
            }

            var senderId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.SenderId = senderId;

            await this.userReportsService.SendAsync(input);

            return this.Redirect("/");
        }
    }
}
