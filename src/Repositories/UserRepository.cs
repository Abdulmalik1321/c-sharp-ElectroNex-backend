using BackendTeamwork.Abstractions;
using BackendTeamwork.Databases;
using BackendTeamwork.Entities;
using BackendTeamwork.Enums;
using Microsoft.EntityFrameworkCore;

namespace BackendTeamwork.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbSet<User> _users;
        private DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _users = databaseContext.User;
            _databaseContext = databaseContext;
        }


        public IEnumerable<User> FindMany(int limit, int offset, SortBy sortBy = SortBy.Ascending)
        {
            IEnumerable<User> sortedUsers;
            if (sortBy == SortBy.Ascending)
            {
                sortedUsers = _users.OrderBy(_users => _users.FirstName);
            }
            else
            {
                sortedUsers = _users.OrderByDescending(_users => _users.FirstName);
            }
            // sorting by Name
            if (limit == 0 && offset == 0)
            {
                return sortedUsers;
            }
            return sortedUsers.Skip(offset).Take(limit);
        }
        public async Task<User?> FindOne(Guid UserId)
        {
            return await _users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == UserId);
        }

        public async Task<User?> FindOneByEmail(string email)
        {
            return await _users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
        }

        public async Task<User> SignUp(User newUser)
        {
            await _users.AddAsync(newUser);
            await _databaseContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> UpdateOne(User updatedUser)
        {
            _users.Update(updatedUser);
            await _databaseContext.SaveChangesAsync();
            return updatedUser;
        }

        public async Task<User?> UpdateRole(Guid userId, Role newRole)
        {
            User? user = await this.FindOne(userId);
            if (user is not null)
            {
                user.Role = newRole;
                _users.Update(user);
                await _databaseContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User> DeleteOne(User deletedUser)
        {
            _users.Remove(deletedUser);
            await _databaseContext.SaveChangesAsync();
            return deletedUser;
        }

        public IEnumerable<User> Search(string searchTerm)
        {
            return _users.Where(user =>
                user.FirstName.ToLower().Contains(searchTerm.ToLower()) ||
                user.LastName.ToLower().Contains(searchTerm.ToLower())
            );
        }
    }
}