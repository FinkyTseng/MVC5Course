namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]
        [Required]
        [必需至少含有一個空白字元(ErrorMessage = "必需至少含有一個空白字元")]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 99, ErrorMessage = "值必須介於0至99之間")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        public Nullable<decimal> Stock { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string Memo { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
