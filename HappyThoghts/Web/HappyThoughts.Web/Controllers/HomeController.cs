namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Web.ViewModels;
    using HappyThoughts.Web.ViewModels.Topics;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ITopicsService topicsService;

        public HomeController(ITopicsService topicsService)
        {
            this.topicsService = topicsService;
        }

        public async Task<IActionResult> Index()
        {
            var topics = await this.topicsService.GetAllAsync<TopicInfoVIewModel>();
            var topicsList = topics.ToList().OrderByDescending(t => t.CreatedOn);
            var viewModel = new TopicsListingViewModel()
            {
                Topics = topicsList,
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
