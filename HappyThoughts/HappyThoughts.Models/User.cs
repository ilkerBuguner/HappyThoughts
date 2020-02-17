using HappyThoughts.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HappyThoughts.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MinLength(3), MaxLength(50), Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        public DateTime RegisteredOn { get; set; }

        public string Location { get; set; }

        public Gender Gender { get; set; }

        public long Reputation { get; set; }

        public UserType UserType { get; set; }

        public byte[] ProfilePicture { get; set; }

        public ICollection<Topic> Posts { get; set; } = new HashSet<Topic>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<Message> MessagesSent { get; set; } = new HashSet<Message>();

        public ICollection<Message> MessagesReceived { get; set; } = new HashSet<Message>();

        public ICollection<SubComment> SubComments { get; set; } = new HashSet<SubComment>();

    }
}
