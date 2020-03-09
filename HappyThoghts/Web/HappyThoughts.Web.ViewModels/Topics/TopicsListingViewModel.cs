namespace HappyThoughts.Web.ViewModels.Topics
{
    using HappyThoughts.Common;
    using HappyThoughts.Web.ViewModels.Categories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TopicsListingViewModel
    {
        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage - 1;

        public int NextPage => this.CurrentPage + 1;

        public bool IsPreviousPageDisabled => this.CurrentPage == 1;

        public bool IsNextPageDisabled
        {
            get
            {
                var maxPage = Math.Ceiling((double)this.TotalTopicsCount / GlobalConstants.DefaultPageSize);

                return maxPage == this.CurrentPage;
            }
        }

        public int TotalTopicsCount { get; set; }

        public IEnumerable<TopicInfoViewModel> Topics { get; set; }

        public IEnumerable<CategoryInfoViewModel> Categories { get; set; }
    }
}
