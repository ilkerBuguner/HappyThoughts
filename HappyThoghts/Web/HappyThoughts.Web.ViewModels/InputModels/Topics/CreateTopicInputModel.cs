namespace HappyThoughts.Web.ViewModels.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using HappyThoughts.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Http;

    public class CreateTopicInputModel
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Content { get; set; }

        public string PictureUrl { get; set; }

        public IFormFile Picture { get; set; }

        public string AuthorId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string CategoryId { get; set; }

        public IEnumerable<CategoryInfoViewModel> Categories { get; set; }
    }
}
