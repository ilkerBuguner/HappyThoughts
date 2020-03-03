using System.ComponentModel.DataAnnotations;

namespace HappyThoughts.Web.ViewModels.InputModels.Comments
{
    public class CreateCommentInputModel
    {
        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string TopicId { get; set; }
    }
}
