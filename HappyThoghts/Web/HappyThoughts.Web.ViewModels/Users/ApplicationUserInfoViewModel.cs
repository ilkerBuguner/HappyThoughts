namespace HappyThoughts.Web.ViewModels.Users
{
    using System;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;

    public class ApplicationUserInfoViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Biography { get; set; }

        public string ProfilePictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsBanned { get; set; }

        public bool IsModerator { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsFollowing { get; set; }

        public int TopicsCount { get; set; }

    }
}
