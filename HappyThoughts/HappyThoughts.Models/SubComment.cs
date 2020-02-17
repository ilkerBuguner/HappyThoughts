using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HappyThoughts.Models
{
    public class SubComment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [ForeignKey("User"), Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        [ForeignKey("Post"), Required]
        public string PostId { get; set; }

        public Topic Post { get; set; }

        [ForeignKey("Comment"), Required]
        public string RootCommentId { get; set; }

        public Comment RootComment { get; set; }
    }
}
