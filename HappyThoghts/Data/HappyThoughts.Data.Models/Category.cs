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
            this.Id = Guid.NewGuid().ToString();
            this.Topics = new HashSet<Topic>();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        [MinLength(3)]
        [MaxLength(40)]
        [Required]
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
