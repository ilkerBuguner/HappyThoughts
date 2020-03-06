namespace HappyThoughts.Web.ViewModels.Topics
{
    using HappyThoughts.Web.ViewModels.Categories;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TopicsListingViewModel
    {
        public IEnumerable<TopicInfoViewModel> Topics { get; set; }

        public IEnumerable<CategoryInfoViewModel> Categories { get; set; }
    }
}
