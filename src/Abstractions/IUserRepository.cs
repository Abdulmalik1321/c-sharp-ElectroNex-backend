using BackendTeamwork.Entities;
using BackendTeamwork.Enums;

namespace BackendTeamwork.Abstractions
{
    public interface IUserRepository
    {
        public IEnumerable<User> FindMany(int limit, int offset, SortBy sortBy);
        public Task<User?> FindOne(Guid id);
        public Task<User?> UpdateRole(Guid userId, Role newRole);

        public Task<User> SignUp(User newUser);
        public Task<User?> FindOneByEmail(string email);
        public Task<User> UpdateOne(User UpdatedUser);
        public Task<User> DeleteOne(User deletedUser);
        public IEnumerable<User> Search(string searchTerm);
    }
}