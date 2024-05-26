#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace BackendTeamwork.DTOs
{
    public class BrandCreateDto
    {
        [Required, StringLength(30)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

    }

    public class BrandReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }

    public class BrandUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

}