using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HappyThoughts.Models
{
    public class Topic
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(50), Required]
        public string Title { get; set; }

        [MinLength(10), Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public byte[] Picture { get; set; }

        public int Views { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [ForeignKey("User"), Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        public ICollection<TopicCategory> Categories { get; set; } = new HashSet<TopicCategory>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<SubComment> SubComments { get; set; } = new HashSet<SubComment>();
    }
}
