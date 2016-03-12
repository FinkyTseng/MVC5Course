using System;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models
{
    public class Product批次更新ViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1,999)]
        public Nullable<decimal> Price { get; set; }

        [Required]
        [Range(1, 999)]
        public Nullable<decimal> Stock { get; set; }

    }
}