
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using BackendTeamwork.Enums;

namespace BackendTeamwork.Abstractions
{
    public interface IProductService
    {
        public IEnumerable<ProductJoinDto> FindMany(int limit, int offset, SortBy sortBy, string searchTerm, string categoryFilter, string brandFiltersString);

        public Task<ProductJoinSingleDto?> FindOne(Guid productId);
        public Task<ProductReadDto?> AddSale(Guid productId, int quantity);

        public Task<ProductReadDto> CreateOne(ProductCreateDto newProduct);

        public Task<ProductReadDto?> UpdateOne(Guid productId, ProductUpdateDto updatedProduct);

        public Task<ProductReadDto?> DeleteOne(Guid productId);

        public IEnumerable<ProductReadDto> Search(string searchTerm);
    }
}