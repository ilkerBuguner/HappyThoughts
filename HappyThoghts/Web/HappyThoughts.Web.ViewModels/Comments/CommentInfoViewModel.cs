namespace HappyThoughts.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.SubComments;

    public class CommentInfoViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public CommentInfoViewModel()
        {
            this.SubComments = new HashSet<SubCommentInfoViewModel>();
        }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }

        public string TopicId { get; set; }

        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<SubCommentInfoViewModel> SubComments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentInfoViewModel>().ForMember(x => x.AuthorName, t => t.MapFrom(opt => opt.Author.UserName));
        }
    }
}
