// ReSharper disable VirtualMemberCallInConstructor
namespace HappyThoughts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HappyThoughts.Data.Common.Models;
    using HappyThoughts.Data.Models.Enumerations;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Posts = new HashSet<Topic>();
            this.Comments = new HashSet<Comment>();
            this.MessagesSent = new HashSet<Message>();
            this.MessagesReceived = new HashSet<Message>();
            this.SubComments = new HashSet<SubComment>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        // Additional info
        public string Location { get; set; }

        public Gender Gender { get; set; }

        public long Reputation { get; set; }

        public UserType UserType { get; set; }

        public string ProfilePictureUrl { get; set; } // byte[]

        public ICollection<Topic> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Message> MessagesSent { get; set; }

        public ICollection<Message> MessagesReceived { get; set; }

        public ICollection<SubComment> SubComments { get; set; }
    }
}
