namespace HappyThoughts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using HappyThoughts.Data.Common.Models;

    public class UserFollower : IDeletableEntity, IAuditInfo
    {
        public UserFollower()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        [ForeignKey("ApplicationUser")]
        [Required]
        public string FollowedUserId { get; set; }

        public virtual ApplicationUser FollowedUser { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required]
        public string FollowingUserId { get; set; }

        public virtual ApplicationUser FollowingUser { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
