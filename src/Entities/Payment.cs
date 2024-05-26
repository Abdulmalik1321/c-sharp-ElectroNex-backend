#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTeamwork.Entities
{
    public class Payment
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required, StringLength(30)]
        public string Method { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, ForeignKey("User")]
        public Guid UserId { get; set; }


        public User User { get; set; }
        public Order Order { get; set; }

    }
}