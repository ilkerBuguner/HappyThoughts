namespace HappyThoughts.Web.ViewModels.InputModels.UserReports
{
    using System.ComponentModel.DataAnnotations;

    public class CreateUserReportInputModel
    {
        [Required]
        [MaxLength(40)]
        public string Title { get; set; }

        [Required]
        [MaxLength(700)]
        public string Description { get; set; }

        public string SenderId { get; set; }

        [Required]
        public string ReportedUserId { get; set; }
    }
}
