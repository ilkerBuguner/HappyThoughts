namespace HappyThoughts.Web.ViewModels.InputModels.Replies
{
    using System.ComponentModel.DataAnnotations;

    public class CreateReplyInputModel
    {
        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        [Required]
        public string TopicId { get; set; }

        [Required]
        public string RootCommentId { get; set; }
    }
}
