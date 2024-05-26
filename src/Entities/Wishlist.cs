#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTeamwork.Entities
{
    public class Wishlist
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }

        public User User { get; set; }
        public IEnumerable<ProductWishlist> Products { get; set; }

    }
}
