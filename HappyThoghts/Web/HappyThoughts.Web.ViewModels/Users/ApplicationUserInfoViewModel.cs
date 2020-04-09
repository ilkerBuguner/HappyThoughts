namespace HappyThoughts.Web.ViewModels.Users
{
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using System;

    public class ApplicationUserInfoViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Biography { get; set; }

        public string ProfilePictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
