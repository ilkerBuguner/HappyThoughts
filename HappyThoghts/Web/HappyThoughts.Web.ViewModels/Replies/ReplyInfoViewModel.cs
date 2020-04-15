namespace HappyThoughts.Web.ViewModels.Replies
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Users;

    public class ReplyInfoViewModel : IMapFrom<Reply>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public ApplicationUserInfoViewModel Author { get; set; }

        public string TopicId { get; set; }

        public string RootCommentId { get; set; }
    }
}
