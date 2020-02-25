namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class TopicsController : BaseController
    {
        private readonly ITopicsService topicsService;

        public TopicsController(ITopicsService topicsService)
        {
            this.topicsService = topicsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateTopicInputModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            input.AuthorId = userId;
            await this.topicsService.CreateAsync(input);

            return this.Redirect("/");
        }

        public IActionResult Details()
        {
            return this.View();
        }
    }
}
