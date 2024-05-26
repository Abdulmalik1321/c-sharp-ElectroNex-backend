#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTeamwork.Entities
{
    public class OrderStock
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }

        [Required, ForeignKey("Order")]
        public Guid OrderId { get; set; }
        [Required, ForeignKey("Stock")]
        public Guid StockId { get; set; }

        public Order Order { get; set; }
        public Stock Stock { get; set; }
    }
}