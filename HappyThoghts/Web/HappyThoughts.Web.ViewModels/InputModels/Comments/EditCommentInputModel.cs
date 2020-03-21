namespace HappyThoughts.Web.ViewModels.InputModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class EditCommentInputModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(2)]
        public string CommentContent { get; set; }

        public string TopicId { get; set; }
    }
}
