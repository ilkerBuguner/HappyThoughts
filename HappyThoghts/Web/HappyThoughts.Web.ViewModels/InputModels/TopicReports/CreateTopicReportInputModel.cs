using System.ComponentModel.DataAnnotations;

namespace HappyThoughts.Web.ViewModels.InputModels.TopicReports
{
    public class CreateTopicReportInputModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string SenderId { get; set; }

        public string TopicId { get; set; }
    }
}
