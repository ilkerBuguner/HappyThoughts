﻿namespace HappyThoughts.Web.ViewModels.Categories
{
    using System;

    using AutoMapper;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;

    public class CategoryInfoViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PictureUrl { get; set; }

        public int TopicsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, CategoryInfoViewModel>().ForMember(x => x.TopicsCount, t => t.MapFrom(opt => opt.Topics.Count));
        }
    }
}
