#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendTeamwork.Enums;
using Microsoft.EntityFrameworkCore;

namespace BackendTeamwork.DTOs
{
    public class UserCreateDto
    {
        public Guid? Id { get; set; }
        [Required(AllowEmptyStrings = false), StringLength(30)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false), StringLength(30)]
        public string LastName { get; set; }
        [Required, StringLength(100), EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [StringLength(15)]
        public string? Phone { get; set; }
    }

    public class UserReadDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
    }

    public class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
    }

    public class UserSignInDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}