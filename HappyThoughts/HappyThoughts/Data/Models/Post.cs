using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HappyThoughts.Data.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [MinLength(10), Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public byte[] Picture { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [ForeignKey("User"), Required]
        public int AuthorId { get; set; }

        public User Author { get; set; }

        [ForeignKey("Category"), Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
