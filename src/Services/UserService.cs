using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using BackendTeamwork.Enums;
using BackendTeamwork.Exceptions;
using BackendTeamwork.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BackendTeamwork.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _UserRepository;
        private IConfiguration _config;
        private IMapper _mapper;

        public UserService(IUserRepository UserRepository, IConfiguration config, IMapper mapper)
        {
            _UserRepository = UserRepository;
            _config = config;
            _mapper = mapper;
        }

        public IEnumerable<UserReadDto> FindMany(int limit, int offset, SortBy sortBy)
        {
            return _UserRepository.FindMany(limit, offset, sortBy).Select(_mapper.Map<UserReadDto>);
        }
        public async Task<UserReadDto?> FindOne(Guid userId)
        {
            return _mapper.Map<UserReadDto>(await _UserRepository.FindOne(userId));
        }

        public async Task<UserReadDto> FindOneByEmail(string email)
        {
            User? user = await _UserRepository.FindOneByEmail(email);
            if (user is null) throw CustomErrorException.NotFound("User not found");
            return _mapper.Map<UserReadDto>(user);
        }


        public async Task<UserReadDto?> SignUp(UserCreateDto newUser)
        {

            User? user = await _UserRepository.FindOneByEmail(newUser.Email);
            if (user is not null) throw CustomErrorException.InvalidData("User already exists");

            Regex regex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            if (!regex.IsMatch(newUser.Email)) throw CustomErrorException.InvalidData("Invalid email format");

            byte[] pepper = Encoding.UTF8.GetBytes(_config["Jwt_Pepper"]!);
            PasswordUtils.HashPassword(newUser.Password, out string hashedPassword, pepper);
            newUser.Password = hashedPassword;

            return _mapper.Map<UserReadDto>(await _UserRepository.SignUp(_mapper.Map<User>(newUser)));
        }

        public async Task<string?> SignIn(UserSignInDto userSignIn)
        {
            User? user = await _UserRepository.FindOneByEmail(userSignIn.Email);
            if (user is null) throw CustomErrorException.NotFound("User does not exist");

            byte[] pepper = Encoding.UTF8.GetBytes(_config["Jwt_Pepper"]!);
            bool isCorrectPass = PasswordUtils.VerifyPassword(userSignIn.Password, user.Password, pepper);

            if (!isCorrectPass) throw CustomErrorException.InvalidData("Invalid data");

            IEnumerable<Claim> claims = [
                // new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            ];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt_SigningKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt_Issuer"],
                audience: _config["Jwt_Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public async Task<UserReadDto?> UpdateOne(Guid userId, UserUpdateDto updatedUser)
        {
            User? targetUser = await _UserRepository.FindOne(userId);
            if (targetUser is null) throw CustomErrorException.NotFound("No user found");

            if (updatedUser.FirstName.Length < 1 || updatedUser.LastName.Length < 1) throw CustomErrorException.InvalidData("Invalid input");
            targetUser.FirstName = updatedUser.FirstName;
            targetUser.LastName = updatedUser.LastName;
            targetUser.Phone = updatedUser.Phone;
            return _mapper.Map<UserReadDto>(await _UserRepository.UpdateOne(targetUser));
        }
        public async Task<UserReadDto?> UpdateRole(Guid userId, Role newRole)
        {
            return _mapper.Map<UserReadDto>(await _UserRepository.UpdateRole(userId, newRole));
        }

        public async Task<UserReadDto?> DeleteOne(Guid userId)
        {
            User? deletedUser = await _UserRepository.FindOne(userId);
            if (deletedUser is null) throw CustomErrorException.NotFound("User not found");

            return _mapper.Map<UserReadDto>(await _UserRepository.DeleteOne(deletedUser));
        }

        public IEnumerable<UserReadDto> Search(string searchTerm)
        {
            return _UserRepository.Search(searchTerm).Select(_mapper.Map<UserReadDto>);
        }

    }
}