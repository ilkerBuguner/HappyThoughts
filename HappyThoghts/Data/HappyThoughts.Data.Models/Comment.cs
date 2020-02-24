namespace HappyThoughts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using HappyThoughts.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.SubComments = new HashSet<SubComment>();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        [Required]
        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        [ForeignKey("Topic")]
        [Required]
        public string TopicId { get; set; }

        public Topic Topic { get; set; }

        public ICollection<SubComment> SubComments { get; set; }
    }
}
