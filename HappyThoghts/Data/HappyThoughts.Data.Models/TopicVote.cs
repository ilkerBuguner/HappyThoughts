namespace HappyThoughts.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HappyThoughts.Data.Common.Models;
    using HappyThoughts.Data.Models.Enumerations;

    public class TopicVote : BaseModel<int>
    {
        [Required]
        public string TopicId { get; set; }

        public virtual Topic Topic { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public VoteType Type { get; set; }
    }
}
