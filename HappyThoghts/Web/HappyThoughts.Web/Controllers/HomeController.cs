namespace HappyThoughts.Web.Controllers
{
    using System.Diagnostics;
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

            var viewModel = new TopicsListingViewModel()
            {
                TotalTopicsCount = this.topicsService.GetTotalTopicsCount(),
                CurrentPage = page,
                Topics = topics,
                Categories = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>(),
            };

            this.ViewData["Title"] = "Home Page";
            this.ViewData["Controller/Action"] = "/Home/Index";

            return this.View(viewModel);
        }

        public async Task<IActionResult> Trend(int page = GlobalConstants.DefaultPageNumber)
        {
            var serviceModel = this.topicsService.GetTrendingTopics(page);

            // var topicsList = topics.ToList().Where(t => t.CreatedOn > DateTime.Now.AddDays(-1)).OrderByDescending(t => t.CreatedOn);
            var viewModel = new TopicsListingViewModel()
            {
                TotalTopicsCount = serviceModel.TotalTopicsCount,
                CurrentPage = page,
                Topics = serviceModel.Topics,
                Categories = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>(),
            };

            this.ViewData["Title"] = "Trending Topics";
            this.ViewData["Controller/Action"] = "/Home/Trend";

            return this.View("Index", viewModel);
        }

        public async Task<IActionResult> Random(int page = GlobalConstants.DefaultPageNumber)
        {
            var topics = this.topicsService.GetRandomTopics(page);

            // var topicsList = topics.ToList().Where(t => t.CreatedOn > DateTime.Now.AddDays(-1)).OrderByDescending(t => t.CreatedOn);
            var viewModel = new TopicsListingViewModel()
            {
                TotalTopicsCount = this.topicsService.GetTotalTopicsCount(),
                CurrentPage = page,
                Topics = topics,
                Categories = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>(),
            };

            this.ViewData["Title"] = "Random Topics";
            this.ViewData["Controller/Action"] = "/Home/Random";

            return this.View("Index", viewModel);
        }

        public async Task<IActionResult> Following(int page = GlobalConstants.DefaultPageNumber)
        {
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var serviceModel = this.topicsService.GetTopicsOfFollowedUsers(currentUserId, page);

            // var topicsList = topics.ToList().Where(t => t.CreatedOn > DateTime.Now.AddDays(-1)).OrderByDescending(t => t.CreatedOn);
            var viewModel = new TopicsListingViewModel()
            {
                TotalTopicsCount = serviceModel.TotalTopicsCount,
                CurrentPage = page,
                Topics = serviceModel.Topics,
                Categories = await this.categoriesService.GetAllAsync<CategoryInfoViewModel>(),
            };

            this.ViewData["Title"] = "Followed Users Topics";
            this.ViewData["Controller/Action"] = "/Home/Following";

            return this.View("Index", viewModel);
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
