using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace FinalProject.Models
{
    public class ProductInfo
    {
        public int Id { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Column(TypeName = "money")]
        public decimal? SalePercent { get; set; }

        public int? SaleBannerId { get; set; }

        public SaleBanner SaleBanner { get; set; }

        [NotMapped]
        public decimal Count { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "bit")]
        public bool isNew { get; set; }

        [Column(TypeName = "bit")]
        public bool isTopSelling { get; set; }

        public DateTime CreateDate { get; set; }

        public int? SizeId { get; set; }

        public int? ColorId { get; set; }

        public Size Size { get; set; }

        public Color Color { get; set; }

        public List<ProductImages> Images { get; set; }

        public List<SizeOption> sizeOptions { get; set; }

        public List<OrderItem> Items { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        [NotMapped]
        public HttpPostedFileBase[] ImageFile { get; set; }

        [NotMapped]
        public string[] SizeOptKey { get; set; }

        [NotMapped]
        public string[] SizeOptValue { get; set; }
    }
}