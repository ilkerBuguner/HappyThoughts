namespace HappyThoughts.Web.ViewModels.Topics
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;

    public class TopicDetailsViewModel : IMapFrom<Topic>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string PictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUsername { get; set; }
    }
}
