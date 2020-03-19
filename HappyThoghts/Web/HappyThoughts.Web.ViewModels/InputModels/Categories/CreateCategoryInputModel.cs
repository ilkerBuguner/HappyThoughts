namespace HappyThoughts.Web.ViewModels.InputModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class CreateCategoryInputModel
    {
        [Required]
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public IFormFile Picture { get; set; }
    }
}
