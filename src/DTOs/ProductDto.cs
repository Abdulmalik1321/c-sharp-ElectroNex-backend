#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BackendTeamwork.Entities
{

    public class ProductCreateDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}