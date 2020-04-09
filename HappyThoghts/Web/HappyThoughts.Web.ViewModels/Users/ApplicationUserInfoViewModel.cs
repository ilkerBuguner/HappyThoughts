namespace HappyThoughts.Web.ViewModels.Users
{
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;

    public class ApplicationUserInfoViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
