namespace HappyThoughts.Web.ViewModels.Topics
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Categories;

    public class TopicInfoViewModel : IMapFrom<Topic>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string PictureUrl { get; set; }

        public int Views { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }

        public string CategoryName { get; set; }

        public int CommentsCount { get; set; }


        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Topic, TopicInfoViewModel>()
                .ForMember(x => x.Content, t => t.MapFrom(opt => opt.Content.Substring(0, 180) + "..."))
                .ForMember(x => x.CategoryName, t => t.MapFrom(opt => opt.Category.Name))
                .ForMember(x => x.CommentsCount, t => t.MapFrom(opt => opt.Comments.Count));
        }
    }
}
