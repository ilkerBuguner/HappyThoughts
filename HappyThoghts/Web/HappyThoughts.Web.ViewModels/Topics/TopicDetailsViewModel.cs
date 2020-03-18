﻿namespace HappyThoughts.Web.ViewModels.Topics
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Ganss.XSS;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Categories;
    using HappyThoughts.Web.ViewModels.Comments;
    using HappyThoughts.Web.ViewModels.Users;

    public class TopicDetailsViewModel : IMapFrom<Topic>
    {
        public TopicDetailsViewModel()
        {
            this.Comments = new HashSet<CommentInfoViewModel>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string PictureUrl { get; set; }

        public int Views { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int CommentsCount => this.Comments.Count;

        public ApplicationUserDetailsViewModel Author { get; set; }

        public ICollection<CommentInfoViewModel> Comments { get; set; }

        public IEnumerable<CategoryInfoViewModel> Categories { get; set; }
    }
}
