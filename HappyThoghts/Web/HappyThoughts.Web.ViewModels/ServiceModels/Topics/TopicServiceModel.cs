namespace HappyThoughts.Web.ViewModels.ServiceModels.Topics
{
    using System.Collections.Generic;

    using HappyThoughts.Web.ViewModels.Topics;

    public class TopicServiceModel
    {
        public int TotalTopicsCount { get; set; }

        public IEnumerable<TopicInfoViewModel> Topics { get; set; }
    }
}
