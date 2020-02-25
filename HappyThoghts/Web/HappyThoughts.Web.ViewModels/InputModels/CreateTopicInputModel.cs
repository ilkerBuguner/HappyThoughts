using System;
using System.Collections.Generic;
using System.Text;

namespace HappyThoughts.Web.ViewModels.InputModels
{
    public class CreateTopicInputModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string PictureUrl { get; set; }

        public string AuthorId { get; set; }
    }
}
