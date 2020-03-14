using System.ComponentModel.DataAnnotations;

namespace HappyThoughts.Web.ViewModels.Topics
{
    public class TopicEditViewModel
    {
        public string Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [MinLength(10)]
        [Required]
        public string Content { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string AuthorId { get; set; }

    }
}
