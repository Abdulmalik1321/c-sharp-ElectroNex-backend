using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Abstractions
{
    public interface IWishlistService
    {
        public IEnumerable<WishlistReadDto> FindMany(Guid userId, int limit, int offset);

        public Task<WishlistReadJoinDto?> FindOne(Guid wishlistId);
        public Task<WishlistReadDto?> FindOneNoJoin(Guid wishlistId);

        public Task<WishlistReadDto?> AddOneProduct(Guid wishlistId, Guid productId);

        public Task<WishlistReadDto> CreateOne(WishlistCreateDto newProduct);

        public Task<WishlistReadDto?> UpdateOne(Guid wishlistId, WishlistUpdateDto updatedWishlist);

        public Task<WishlistReadDto?> DeleteOne(Guid wishlistId);
    }
}