namespace HappyThoughts.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.InputModels.Replies;
    using HappyThoughts.Web.ViewModels.Replies;
    using HappyThoughts.Web.ViewModels.Users;

    public class CommentInfoViewModel : IMapFrom<Comment>
    {
        public CommentInfoViewModel()
        {
            this.Replies = new HashSet<ReplyInfoViewModel>();
        }

        public string Id { get; set; }

        public string TopicId { get; set; }

        [Required]
        [MinLength(2)]
        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime CreatedOn { get; set; }

        public ApplicationUserInfoViewModel Author { get; set; }

        public ICollection<ReplyInfoViewModel> Replies { get; set; }

        public EditReplyInputModel ReplyEditInputModel { get; set; }
    }
}
