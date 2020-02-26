namespace HappyThoughts.Web.ViewModels.InputModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateCategoryInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
