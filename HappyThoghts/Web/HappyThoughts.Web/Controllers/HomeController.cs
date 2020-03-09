namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using HappyThoughts.Common;
    using HappyThoughts.Services.Data.Categories;
    using HappyThoughts.Services.Data.Topics;
    using HappyThoughts.Web.ViewModels;
    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.Errors;
    using HappyThoughts.Web.ViewModels.Topics;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ITopicsService topicsService;
        private readonly ICategoriesService categoriesService;

        public HomeController(ITopicsService topicsService, ICategoriesService categoriesService)
        {
            this.topicsService = topicsService;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index(int page = GlobalConstants.DefaultPageNumber)
        {
            var topics = this.topicsService.GetLatestTopics(page);

            // var topicsList = topics.ToList().Where(t => t.CreatedOn > DateTime.Now.AddDays(-1)).OrderByDescending(t => t.CreatedOn);
            var viewModel = new TopicsListingViewModel()
            {
                TotalTopicsCount = this.topicsService.GetTotalTopicsCount(),
                CurrentPage = page,
                Topics = topics,
                Categories = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>(),
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

        public IActionResult HttpError(int statusCode)
        {
            var viewModel = new HttpErrorViewModel
            {
                StatusCode = statusCode,
            };

            if (statusCode == 404)
            {
                return this.View(viewModel);
            }

            return this.View(
                "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
