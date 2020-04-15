namespace HappyThoughts.Web.ViewModels.Topics
{
    using System;
    using System.Collections.Generic;

    using HappyThoughts.Common;
    using HappyThoughts.Web.ViewModels.Categories;

    public class TopicsListingViewModel
    {
        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage - 1;

        public int NextPage => this.CurrentPage + 1;

        public bool IsPreviousPageDisabled => this.CurrentPage == 1;

        public int MaxPages => (int)Math.Ceiling((double)this.TotalTopicsCount / GlobalConstants.DefaultPageSize);

        public bool IsNextPageDisabled
        {
            get
            {
                var maxPages = Math.Ceiling((double)this.TotalTopicsCount / GlobalConstants.DefaultPageSize);

                return maxPages == this.CurrentPage;
            }
        }

        public int TotalTopicsCount { get; set; }

        public IEnumerable<TopicInfoViewModel> Topics { get; set; }

        public IEnumerable<CategoryInfoViewModel> Categories { get; set; }
    }
}
