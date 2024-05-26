using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using BackendTeamwork.Enums;

namespace BackendTeamwork.Abstractions
{
    public interface IUserService
    {
        public IEnumerable<UserReadDto> FindMany(int limit, int offset, SortBy sortBy);
        public Task<UserReadDto?> FindOne(Guid userId);
        public Task<UserReadDto?> UpdateRole(Guid userId, Role newRole);
        public Task<UserReadDto> FindOneByEmail(string email);

        public Task<UserReadDto?> SignUp(UserCreateDto newUser);

        public Task<string?> SignIn(UserSignInDto userSignIn);
        public Task<UserReadDto?> UpdateOne(Guid userId, UserUpdateDto UpdatedUser);
        public Task<UserReadDto?> DeleteOne(Guid userId);
        public IEnumerable<UserReadDto> Search(string searchTerm);

    }
}