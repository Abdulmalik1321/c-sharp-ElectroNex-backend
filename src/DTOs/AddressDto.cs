#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTeamwork.DTOs
{
    public class AddressCreateDto
    {
        [Required, StringLength(30)]
        public string City { get; set; }
        [Required, StringLength(10)]
        public string? Zip { get; set; }
        [StringLength(100)]
        public string? AddressLine { get; set; }
        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }
    }

    public class AddressReadDto
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string? Zip { get; set; }
        public string? AddressLine { get; set; }
        public Guid UserId { get; set; }
    }

    public class AddressUpdateDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required, StringLength(30)]
        public string City { get; set; }
        [Required, StringLength(10)]
        public string? Zip { get; set; }
        [StringLength(100)]
        public string? AddressLine { get; set; }
        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }
    }
}