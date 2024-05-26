#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTeamwork.DTOs
{
    public class StockImageCreateDto
    {
        [Required, ForeignKey("Stock")]
        public Guid StockId { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public bool IsMain { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Size { get; set; }

    }

    public class StockImageWithoutIdDto
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public bool IsMain { get; set; }
    }

    public class StockImageReadDto
    {
        [Required, ForeignKey("Stock")]
        public Guid StockId { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public bool IsMain { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Quantity { get; set; }
        public string UserName { get; set; }
        public string Condition { get; set; }

    }

    public class StockImageUpdateDto
    {
        public Guid Id { get; set; }

        [Required, ForeignKey("Stock")]
        public Guid StockId { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Size { get; set; }
    }

}