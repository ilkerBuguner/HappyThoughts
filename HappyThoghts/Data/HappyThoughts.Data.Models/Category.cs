namespace HappyThoughts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using HappyThoughts.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.Posts = new HashSet<TopicCategory>();
        }

        [MinLength(3)]
        [MaxLength(40)]
        [Required]
        public string Name { get; set; }

        public ICollection<TopicCategory> Posts { get; set; }
    }
}
