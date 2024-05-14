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
    public class BrandRepository : IBrandRepository
    {

        private DbSet<Brand> _brands;
        private DatabaseContext _databaseContext;

        public BrandRepository(DatabaseContext databaseContext)
        {
            _brands = databaseContext.Brand;
            _databaseContext = databaseContext;

        }


        public IEnumerable<Brand> FindMany(int limit, int offset)
        {
            return _brands.Skip(offset).Take(limit).ToList();
        }

        public async Task<Brand?> FindOne(Guid id)
        {
            return await _brands.AsNoTracking().FirstOrDefaultAsync(category => category.Id == id);
        }

        public async Task<Brand> CreateOne(Brand newBrand)
        {
            await _brands.AddAsync(newBrand);
            await _databaseContext.SaveChangesAsync();
            return newBrand;
        }

        public async Task<Brand> UpdateOne(Brand updateBrand)
        {
            _brands.Update(updateBrand);
            await _databaseContext.SaveChangesAsync();
            return updateBrand;
        }

        public async Task<Brand> DeleteOne(Brand deletedBrand)
        {
            _brands.Remove(deletedBrand);
            await _databaseContext.SaveChangesAsync();
            return deletedBrand;
        }

    }
}