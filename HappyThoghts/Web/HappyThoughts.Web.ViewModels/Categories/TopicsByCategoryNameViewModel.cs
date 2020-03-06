using HappyThoughts.Web.ViewModels.Topics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyThoughts.Web.ViewModels.Categories
{
    public class TopicsByCategoryNameViewModel
    {
        public string CategoryName { get; set; }

        public TopicsListingViewModel CategoryTopics { get; set; }
    }
}
