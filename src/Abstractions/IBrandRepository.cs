using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Abstractions
{
    public interface IBrandRepository
    {

        public IEnumerable<Brand> FindMany(int limit, int offset);
        public Task<Brand?> FindOne(Guid id);
        public Task<Brand> CreateOne(Brand newBrand);

        public Task<Brand> UpdateOne(Brand updateBrand);

        public Task<Brand> DeleteOne(Brand deletedBrand);

    }
}