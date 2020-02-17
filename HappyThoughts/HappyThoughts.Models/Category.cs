using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HappyThoughts.Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MinLength(3), MaxLength(40), Required]
        public string Name { get; set; }

        public ICollection<TopicCategory> Posts { get; set; } = new HashSet<TopicCategory>();
    }
}
