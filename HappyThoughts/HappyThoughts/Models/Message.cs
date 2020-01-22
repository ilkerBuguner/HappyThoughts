using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HappyThoughts.Data.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [ForeignKey("User"), Required]
        public int SenderId { get; set; }

        public User Sender { get; set; }

        [ForeignKey("User"), Required]
        public int ReceiverId { get; set; }

        public User Receiver { get; set; }

    }
}
