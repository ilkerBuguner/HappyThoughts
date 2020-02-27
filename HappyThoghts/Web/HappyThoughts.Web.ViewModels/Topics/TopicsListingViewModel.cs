using System;
using System.Collections.Generic;
using System.Text;

namespace HappyThoughts.Web.ViewModels.Topics
{
    public class TopicsListingViewModel
    {
        public IEnumerable<TopicInfoVIewModel> Topics { get; set; }
    }
}
