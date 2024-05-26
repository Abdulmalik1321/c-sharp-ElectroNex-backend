#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTeamwork.DTOs
{
    public class WishlistCreateDto
    {
        [Required, StringLength(30)]
        public string Name { get; set; }
        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }
    }

    public class WishlistReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }

    public class WishlistReadJoinDto
    {

        public string WishlistName { get; set; }
        public IEnumerable<ProductJoinDto> Products { get; set; }
    }
    public class WishlistUpdateDto
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}
