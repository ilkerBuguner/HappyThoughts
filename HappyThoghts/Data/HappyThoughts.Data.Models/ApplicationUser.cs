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

            this.Topics = new HashSet<Topic>();
            this.Comments = new HashSet<Comment>();
            this.MessagesSent = new HashSet<Message>();
            this.MessagesReceived = new HashSet<Message>();
            this.Replies = new HashSet<Reply>();
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
        [PersonalData]
        [MaxLength(50)]
        public string FullName { get; set; }

        [PersonalData]
        [MaxLength(300)]
        public string Biography { get; set; }

        [PersonalData]
        public DateTime Birthday { get; set; }

        [PersonalData]
        [MaxLength(100)]
        public string Location { get; set; }

        [PersonalData]
        public Gender Gender { get; set; }

        public long Reputation { get; set; }

        public UserType UserType { get; set; }

        public string ProfilePictureUrl { get; set; } // byte[]

        public virtual ICollection<Topic> Topics { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Message> MessagesSent { get; set; }

        public virtual ICollection<Message> MessagesReceived { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public virtual ICollection<UserReport> ReportsSent { get; set; }

        public virtual ICollection<UserReport> ReceivedReports { get; set; }
    }
}
