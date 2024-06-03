#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTeamwork.DTOs
{
    public class StockCreateDto
    {
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

        public IEnumerable<StockImageWithoutIdDto> Images { get; set; }

    }
    public class StockCreateDtoWithoutId
    {
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
        public Guid UserId { get; set; }
        public IEnumerable<StockImageWithoutIdDto> Images { get; set; }

    }

    public class StockJoinManyDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public IEnumerable<StockImagesDto> Images { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public string UserName { get; set; }
        public string Condition { get; set; }
    }

    public class StockImagesDto
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }

    public class StockReadDto
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

    }

    public class StockReadWithImgDto
    {
        public Guid StockId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }

    }

    public class StockUpdateDto
    {

        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Condition { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

    }
}