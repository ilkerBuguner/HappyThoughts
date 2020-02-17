using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HappyThoughts.Models
{
    public class TopicCategory
    {
        [ForeignKey("Post"), Required]
        public string PostId { get; set; }

        public Topic Post { get; set; }

        [ForeignKey("Category"), Required]
        public string CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
