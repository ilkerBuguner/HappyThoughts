using System;
using System.Collections.Generic;
using System.Text;

namespace HappyThoughts.Web.ViewModels.Forums
{
    public class ForumStatsViewModel
    {
        public int TotalTopicsCount { get; set; }

        public int TotalUsersCount { get; set; }

        public int TotalAdminsCount { get; set; }

        public int TotalModeratorsCount { get; set; }

        public int TotalCategoriesCount { get; set; }
    }
}
