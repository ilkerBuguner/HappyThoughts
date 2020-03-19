namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using HappyThoughts.Services.Data.TopicReports;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Web.ViewModels.InputModels.TopicReports;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TopicReportsController : BaseController
    {
        private readonly ITopicsService topicsService;
        private readonly ITopicReportsService topicReportsService;

        public TopicReportsController(ITopicsService topicsService, ITopicReportsService topicReportsService)
        {
            this.topicsService = topicsService;
            this.topicReportsService = topicReportsService;
        }

        [Authorize]
        public async Task<IActionResult> Send(string topicId)
        {
            var topic = await this.topicsService.GetByIdAsViewModelAsync(topicId);
            this.ViewData["TopicId"] = topic.Id;
            this.ViewData["TopicTitle"] = topic.Title;

            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Send(CreateTopicReportInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/TopicReports/Send?topicId={input.TopicId}");
            }

            var senderId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.SenderId = senderId;

            await this.topicReportsService.SendAsync(input);

            return this.Redirect("/");
        }
    }
}
