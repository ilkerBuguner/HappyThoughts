namespace HappyThoughts.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using HappyThoughts.Data.Common.Models;

    public class TopicCategory : BaseDeletableModel<string>
    {
        [ForeignKey("Post")]
        [Required]
        public string PostId { get; set; }

        public Topic Post { get; set; }

        [ForeignKey("Category")]
        [Required]
        public string CategoryId { get; set; }

        public Category Category { get; set; }
    }
}