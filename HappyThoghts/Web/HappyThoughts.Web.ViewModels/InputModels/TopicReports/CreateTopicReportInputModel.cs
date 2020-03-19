namespace HappyThoughts.Web.ViewModels.InputModels.TopicReports
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTopicReportInputModel
    {
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        [MaxLength(700)]
        public string Description { get; set; }

        public string SenderId { get; set; }

        public string TopicId { get; set; }
    }
}
