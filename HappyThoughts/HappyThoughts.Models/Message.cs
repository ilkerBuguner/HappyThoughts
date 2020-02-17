using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HappyThoughts.Models
{
    public class Message
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public bool Seen { get; set; }

        [ForeignKey("User"), Required]
        public string SenderId { get; set; }

        public User Sender { get; set; }

        [ForeignKey("User"), Required]
        public string ReceiverId { get; set; }

        public User Receiver { get; set; }
    }
}
