using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HappyThoughts.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3), MaxLength(40), Required]
        public string Name { get; set; }

    }
}
