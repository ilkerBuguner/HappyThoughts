namespace HappyThoughts.Web.ViewModels.ServiceModels.Topics
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HappyThoughts.Web.ViewModels.Topics;

    public class TopicServiceModel
    {
        public int TotalTopicsCount { get; set; }

        public IEnumerable<TopicInfoViewModel> Topics { get; set; }
    }
}
