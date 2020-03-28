using AutoMapper;
using HappyThoughts.Data.Models;
using HappyThoughts.Services.Mapping;
using System;

namespace HappyThoughts.Web.ViewModels.UserReports
{
    public class UserReportDetailsViewModel : IMapFrom<UserReport>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SenderId { get; set; }

        public string ReportedUserId { get; set; }

        public DateTime SendOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserReport, UserReportDetailsViewModel>()
                .ForMember(x => x.SendOn, t => t.MapFrom(opt => opt.CreatedOn));
        }
    }
}
