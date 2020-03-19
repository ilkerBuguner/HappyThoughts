namespace HappyThoughts.Web.ViewModels.TopicReports
{
    using System;

    using AutoMapper;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;

    public class TopicReportDetailsViewModel : IMapFrom<TopicReport>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SenderId { get; set; }

        public string TopicId { get; set; }

        public DateTime SendOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<TopicReport, TopicReportDetailsViewModel>()
                .ForMember(x => x.SendOn, t => t.MapFrom(opt => opt.CreatedOn));
        }
    }
}
