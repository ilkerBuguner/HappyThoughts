namespace HappyThoughts.Web.ViewModels.Reports
{
    using System.Collections.Generic;

    using HappyThoughts.Web.ViewModels.TopicReports;
    using HappyThoughts.Web.ViewModels.UserReports;

    public class ReportsListingViewModel
    {
        public ReportsListingViewModel()
        {
            //this.TopicReports = new HashSet<TopicReportDetailsViewModel>();
            //this.UserReports = new HashSet<UserReportDetailsViewModel>();
        }

        public IEnumerable<TopicReportDetailsViewModel> TopicReports { get; set; }

        public IEnumerable<UserReportDetailsViewModel> UserReports { get; set; }
    }
}
