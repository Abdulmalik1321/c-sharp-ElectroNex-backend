using System.Reflection;
using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using Microsoft.AspNetCore.Authorization;
using BackendTeamwork.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendTeamwork.Controllers
{
    public class UsersController : BaseController
    {
        private IUserService _userService;

        public UsersController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserReadDto>> FindMany([FromQuery(Name = "limit")] int limit, [FromQuery(Name = "offset")] int offset, [FromQuery(Name = "sort")] SortBy sortBy)
        {
            return Ok(_userService.FindMany(limit, offset, sortBy));
        }

        [HttpGet(":{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadDto>> FindOne(Guid userId)
        {
            return Ok(await _userService.FindOne(userId));
        }

        [HttpGet("email/{email}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadDto>> FindOneByEmail(string email)
        {
            var emailFromToken = User.FindFirstValue(ClaimTypes.Email);
            Console.WriteLine($"{emailFromToken}");
            Console.WriteLine($"{email}");

            var userRole = User.FindFirstValue(ClaimTypes.Role);
            Console.WriteLine($"{userRole}");

            if (emailFromToken != email && userRole == Role.Customer.ToString())
            {
                return NotFound();
            }
            return Ok(await _userService.FindOneByEmail(email));
        }

        [HttpPost("signUp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserReadDto>> SignUp([FromBody] UserCreateDto newUser)
        {
            return Ok(await _userService.SignUp(newUser));
        }

        [HttpPost("signIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserReadDto>> SignIn([FromBody] UserSignInDto userSignIn)
        {
            return Ok(await _userService.SignIn(userSignIn));
        }

        [HttpPut(":{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadDto>> UpdateOne(Guid userId, [FromBody] UserUpdateDto updatedUser)
        {
            return Ok(await _userService.UpdateOne(userId, updatedUser));
        }

        [HttpDelete(":{userId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadDto>> DeleteOne(Guid userId)
        {
            await _userService.DeleteOne(userId);
            return NoContent();
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserReadDto>> Search(string searchTerm)
        {
            return Ok(_userService.Search(searchTerm));
        }


    }
}