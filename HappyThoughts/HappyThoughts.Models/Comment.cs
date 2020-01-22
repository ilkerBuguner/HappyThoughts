using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HappyThoughts.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [ForeignKey("User"), Required]
        public int AuthorId { get; set; }

        public User Author { get; set; }

        [ForeignKey("Post"), Required]
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
