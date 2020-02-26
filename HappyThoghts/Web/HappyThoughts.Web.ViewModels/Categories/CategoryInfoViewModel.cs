using HappyThoughts.Data.Models;
using HappyThoughts.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace HappyThoughts.Web.ViewModels.Categories
{
    public class CategoryInfoViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
