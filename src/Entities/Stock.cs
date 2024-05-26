#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTeamwork.Entities
{
    public class Stock
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required, StringLength(30)]
        public string Size { get; set; }

        [Required, StringLength(30)]
        public string Color { get; set; }

        [Required, StringLength(30)]
        public string Condition { get; set; }

        [Required, ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }

        public IEnumerable<OrderStock> OrderStocks { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }

        public IEnumerable<StockImage> StockImage { get; set; }
    }
}