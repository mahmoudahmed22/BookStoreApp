using Microsoft.AspNetCore.Http;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace Book.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        [Required(ErrorMessage = "The Book Name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        [Required(ErrorMessage = "The Book Price is required.")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
    }
}
