using System.ComponentModel.DataAnnotations;

namespace HappyThoughts.Web.ViewModels.InputModels.Comments
{
    public class CreateCommentInputModel
    {
        [Required]
        [MinLength(2)]
        public string CommentContent { get; set; }

        public string AuthorId { get; set; }

        public string TopicId { get; set; }
    }
}
