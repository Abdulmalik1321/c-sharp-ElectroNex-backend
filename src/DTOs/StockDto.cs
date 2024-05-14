#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTeamwork.DTOs
{
    public class StockCreateDto
    {
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required, StringLength(30)]
        public string Size { get; set; }
        [Required, StringLength(30)]
        public string Color { get; set; }
        [Required, ForeignKey("Product")]
        public Guid ProductId { get; set; }
        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }

    }
    public class StockCreateDtoWithoutId
    {
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required, StringLength(30)]
        public string Size { get; set; }
        [Required, StringLength(30)]
        public string Color { get; set; }
        public Guid UserId { get; set; }


    }

    public class StockReadDto
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

    }

    public class StockUpdateDto
    {
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

    }
}