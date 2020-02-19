namespace HappyThoughts.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    using HappyThoughts.Data.Common.Models;

    public class Message : BaseDeletableModel<string>
    {
        [Required]
        public string Content { get; set; }

        public bool Seen { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required]
        public string SenderId { get; set; }

        public ApplicationUser Sender { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required]
        public string ReceiverId { get; set; }

        public ApplicationUser Receiver { get; set; }
    }
}
