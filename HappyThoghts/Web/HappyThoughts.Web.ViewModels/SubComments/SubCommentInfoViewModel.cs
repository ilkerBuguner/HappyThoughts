namespace HappyThoughts.Web.ViewModels.SubComments
{
    using System;

    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.Users;

    public class SubCommentInfoViewModel : IMapFrom<Reply>
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public ApplicationUserDetailsViewModel Author { get; set; }

        public string TopicId { get; set; }

        public string RootCommentId { get; set; }
    }
}
