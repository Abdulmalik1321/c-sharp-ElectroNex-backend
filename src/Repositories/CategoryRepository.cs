using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTeamwork.Abstractions;
using BackendTeamwork.Databases;
using BackendTeamwork.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTeamwork.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private DbSet<Category> _categories;
        private DatabaseContext _databaseContext;

        public CategoryRepository(DatabaseContext databaseContext)
        {
            _categories = databaseContext.Category;
            _databaseContext = databaseContext;

        }


        public IEnumerable<Category> FindMany(int limit, int offset)
        {
            return _categories;
        }

        public async Task<Category?> FindOne(Guid id)
        {
            return await _categories.AsNoTracking().FirstOrDefaultAsync(category => category.Id == id);
        }

        public async Task<Category> CreateOne(Category newCategory)
        {
            await _categories.AddAsync(newCategory);
            await _databaseContext.SaveChangesAsync();
            return newCategory;
        }

        public async Task<Category> UpdateOne(Category updateCategory)
        {
            _categories.Update(updateCategory);
            await _databaseContext.SaveChangesAsync();
            return updateCategory;
        }

        public async Task<Category> DeleteOne(Category deletedCategory)
        {
            _categories.Remove(deletedCategory);
            await _databaseContext.SaveChangesAsync();
            return deletedCategory;
        }

    }
}