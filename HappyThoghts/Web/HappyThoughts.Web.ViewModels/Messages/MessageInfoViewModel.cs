namespace HappyThoughts.Web.ViewModels.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using HappyThoughts.Data.Models;
    using HappyThoughts.Services.Mapping;

    public class MessageInfoViewModel : IMapFrom<Message>, IHaveCustomMappings
    {
        public string Content { get; set; }

        public string ShortContent
        {
            get
            {
                return this.Content.Length > 120
                        ? this.Content.Substring(0, 120) + "..."
                        : this.Content;

            }
        }

        public bool Seen { get; set; }

        public DateTime SentOn { get; set; }

        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, MessageInfoViewModel>()
                .ForMember(x => x.SentOn, t => t.MapFrom(opt => opt.CreatedOn));
        }
    }
}
