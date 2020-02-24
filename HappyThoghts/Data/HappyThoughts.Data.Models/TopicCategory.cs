namespace HappyThoughts.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using HappyThoughts.Data.Common.Models;

    public class TopicCategory : BaseDeletableModel<string>
    {
        public TopicCategory()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        [ForeignKey("Topic")]
        [Required]
        public string TopicId { get; set; }

        public Topic Topic { get; set; }

        [ForeignKey("Category")]
        [Required]
        public string CategoryId { get; set; }

        public Category Category { get; set; }
    }
}