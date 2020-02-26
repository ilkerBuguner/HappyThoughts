using AutoMapper;
using HappyThoughts.Data.Models;
using HappyThoughts.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyThoughts.Web.ViewModels.Topics
{
    public class TopicInfoVIewModel : IMapFrom<Topic>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string PictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Topic, TopicInfoVIewModel>().ForMember(x => x.Content, t => t.MapFrom(opt => opt.Content.Substring(0, 220) + "..."));
        }
    }
}
