#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BackendTeamwork.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }


        [Required, StringLength(500)]
        public string Description { get; set; }

        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumberOfSales { get; set; } = 0;

        [Required, ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        [Required, ForeignKey("Brand")]
        public Guid BrandId { get; set; }

        public Category Category { get; set; }
        public Brand Brand { get; set; }
        public IEnumerable<ProductWishlist> Wishlists { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Stock> Stocks { get; set; }


    }
}