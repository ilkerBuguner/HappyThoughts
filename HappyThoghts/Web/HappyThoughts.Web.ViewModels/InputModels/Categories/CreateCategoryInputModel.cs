namespace HappyThoughts.Web.ViewModels.InputModels.Categories
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateCategoryInputModel
    {
        [Required]
        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public IFormFile Picture { get; set; }
    }
}
