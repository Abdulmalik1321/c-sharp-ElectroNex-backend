#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace BackendTeamwork.Entities
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }


        public IEnumerable<Product> Products { get; set; }


    }

}