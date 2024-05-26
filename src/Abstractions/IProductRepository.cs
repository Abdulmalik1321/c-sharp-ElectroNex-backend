
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using BackendTeamwork.Enums;

namespace BackendTeamwork.Abstractions
{
    public interface IProductRepository
    {
        public IEnumerable<ProductJoinDto> FindMany(int limit, int offset, SortBy sortBy, string searchTerm, string categoryFilter, string brandFiltersString);
        public Task<ProductJoinSingleDto?> FindOne(Guid productId);
        public Task<Product> AddSale(Guid productId, int quantity);

        public Task<Product?> FindOneNoJoin(Guid productId);

        public Task<Product> CreateOne(Product newProduct);

        public Task<Product> UpdateOne(Product updatedProduct);

        public Task<Product> DeleteOne(Product targetProducts);
        public IEnumerable<Product> Search(string searchTerm);
    }
}