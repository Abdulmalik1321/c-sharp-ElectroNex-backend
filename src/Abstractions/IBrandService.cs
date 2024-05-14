using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Abstractions
{
    public interface IBrandService
    {

        public IEnumerable<BrandReadDto> FindMany(int limit, int offset);
        public Task<BrandReadDto?> FindOne(Guid id);
        public Task<BrandReadDto> CreateOne(BrandCreateDto newBrand);

        public Task<BrandReadDto?> UpdateOne(Guid brandId, BrandUpdateDto updateBrand);

        public Task<BrandReadDto?> DeleteOne(Guid brandId);
    }
}