namespace HappyThoughts.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using HappyThoughts.Data.Common.Models;
    using HappyThoughts.Data.Models.Enumerations;

    public class TopicVote : BaseModel<int>
    {
        public TopicVote()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [ForeignKey("Topic")]
        [Required]
        public string TopicId { get; set; }

        public virtual Topic Topic { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public VoteType Type { get; set; }
    }
}
