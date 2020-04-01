namespace HappyThoughts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using HappyThoughts.Data.Common.Models;

    public class Topic : BaseDeletableModel<string>
    {
        public Topic()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;

            this.Comments = new HashSet<Comment>();
            this.Replies = new HashSet<Reply>();
        }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [MinLength(10)]
        [Required]
        public string Content { get; set; }

        public string PictureUrl { get; set; } // byte[]

        public int Views { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [ForeignKey("Category")]
        // [Required]
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        // public ICollection<TopicCategory> Categories { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
    }
}
