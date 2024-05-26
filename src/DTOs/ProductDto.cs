#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BackendTeamwork.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTeamwork.DTOs
{

    public class ProductCreateDto
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }
        [Required, ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        [Required, ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public string Status { get; set; }

        public IEnumerable<StockCreateDtoWithoutId>? NewStocks { get; set; }
    }

    public class ProductJoinDto
    {
        public Guid Id { get; set; }
        public IEnumerable<string> Colors { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Sizes { get; set; }
        public IEnumerable<IEnumerable<string>> Images { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumberOfSales { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }

        public string Description { get; set; }
    }

    public class ProductJoinSingleDto
    {
        public Guid Id { get; set; }
        public IEnumerable<string> Colors { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Sizes { get; set; }
        public IEnumerable<StockImageReadDto> Stocks { get; set; }

        public double Price { get; set; }
        public string Description { get; set; }
    }

    public class ProductReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }

    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }

    public class ProductCategoryFilter
    {
        public IEnumerable<string> Category { get; set; }
    }
}