namespace HappyThoughts.Web.ViewModels.InputModels.Replies
{
    using System.ComponentModel.DataAnnotations;

    public class EditReplyInputModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Content { get; set; }

        [Required]
        public string TopicId { get; set; }
    }
}
