namespace HappyThoughts.Web.ViewModels.Comments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;
    using HappyThoughts.Web.ViewModels.SubComments;
    using HappyThoughts.Web.ViewModels.Users;

    public class CommentInfoViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public CommentInfoViewModel()
        {
            this.SubComments = new HashSet<SubCommentInfoViewModel>();
        }

        public string Id { get; set; }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }

        public string TopicId { get; set; }

        [Required]
        [MinLength(2)]
        public string Content { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime CreatedOn { get; set; }

        public ApplicationUserDetailsViewModel Author { get; set; }

        public ICollection<SubCommentInfoViewModel> SubComments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentInfoViewModel>().ForMember(x => x.AuthorName, t => t.MapFrom(opt => opt.Author.UserName));
        }
    }
}
