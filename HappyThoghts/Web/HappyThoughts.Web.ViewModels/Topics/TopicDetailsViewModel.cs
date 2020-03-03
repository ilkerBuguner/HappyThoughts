namespace HappyThoughts.Web.ViewModels.Topics
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.Comments;

    public class TopicDetailsViewModel : IMapFrom<Topic>
    {
        public TopicDetailsViewModel()
        {
            this.Comments = new HashSet<CommentInfoViewModel>();
        }

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

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public ICollection<CommentInfoViewModel> Comments { get; set; }

        public IEnumerable<CategoryInfoViewModel> Categories { get; set; }
    }
}
