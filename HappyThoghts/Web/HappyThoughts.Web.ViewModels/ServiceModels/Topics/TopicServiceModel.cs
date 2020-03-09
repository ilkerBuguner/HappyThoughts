using HappyThoughts.Web.ViewModels.Topics;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyThoughts.Web.ViewModels.ServiceModels.Topics
{
    public class TopicServiceModel
    {
        public int TotalTopicsCount { get; set; }

        public IEnumerable<TopicInfoViewModel> Topics { get; set; }
    }
}
