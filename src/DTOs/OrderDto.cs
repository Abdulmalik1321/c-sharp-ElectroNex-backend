#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendTeamwork.Entities;

namespace BackendTeamwork.DTOs
{
    public class OrderCreateDto
    {
        [Required, StringLength(30)]
        public string Status { get; set; }
        [Required, ForeignKey("Payment")]
        public Guid PaymentId { get; set; }
        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }
    }

    public class OrderReadDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public Guid PaymentId { get; set; }
        public Guid UserId { get; set; }
    }

    public class OrderJoinDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<StockReadWithImgDto> Stocks { get; set; }
    }

    public class OrderUpdateDto
    {
        public string Status { get; set; }
    }
}