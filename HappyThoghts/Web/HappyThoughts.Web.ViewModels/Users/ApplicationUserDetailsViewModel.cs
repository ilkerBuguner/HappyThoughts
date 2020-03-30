namespace HappyThoughts.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Topics;

    public class ApplicationUserDetailsViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string CreatedOn { get; set; }

        public string UserName { get; set; }

        public string Birthday { get; set; }

        public string FullName { get; set; }

        public string Biography { get; set; }

        public string Location { get; set; }

        public string Gender { get; set; }

        public long Reputation { get; set; }

        public string UserType { get; set; }

        public string ProfilePictureUrl { get; set; }

        public bool IsBanned { get; set; }

        public bool IsAdmin { get; set; }

        public virtual ICollection<TopicInfoViewModel> Topics { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, ApplicationUserDetailsViewModel>()
                .ForMember(u => u.CreatedOn, t => t.MapFrom(opt => opt.CreatedOn.ToString("d")))
                .ForMember(u => u.Birthday, t => t.MapFrom(opt => opt.Birthday.ToString("d")));

        }
    }
}
