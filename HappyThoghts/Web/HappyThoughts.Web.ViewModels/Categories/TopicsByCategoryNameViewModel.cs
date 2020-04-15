namespace HappyThoughts.Web.ViewModels.Categories
{
    using HappyThoughts.Web.ViewModels.Topics;

    public class TopicsByCategoryNameViewModel
    {
        public string CategoryName { get; set; }

        public string PictureUrl { get; set; }

        public TopicsListingViewModel CategoryTopics { get; set; }
    }
}
