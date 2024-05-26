#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTeamwork.Entities
{
    public class StockImage
    {
        [Key]
        public Guid Id { get; set; }

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

        [Required, StringLength(30)]
        public Stock Stock { get; set; }

    }

}