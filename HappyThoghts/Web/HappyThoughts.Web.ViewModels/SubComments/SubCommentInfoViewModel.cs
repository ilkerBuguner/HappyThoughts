namespace HappyThoughts.Web.ViewModels.SubComments
{
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;

    public class SubCommentInfoViewModel : IMapFrom<SubComment>
    {
        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string TopicId { get; set; }

        public string RootCommentId { get; set; }
    }
}
