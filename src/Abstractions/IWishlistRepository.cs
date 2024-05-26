using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Abstractions
{
    public interface IWishlistRepository
    {
        public IEnumerable<Wishlist> FindMany(Guid userId, int limit, int offset);
        public Task<WishlistReadJoinDto?> FindOne(Guid wishlistId);
        public Task<Wishlist?> FindOneNoJoin(Guid wishlistId);

        public Task<Wishlist> AddOneProduct(Guid wishlist, Guid product);
        public Task<Wishlist> CreateOne(Wishlist newWishlist);

        public Task<Wishlist> UpdateOne(Wishlist updatedWishlist);

        public Task<Wishlist> DeleteOne(Wishlist targetWishlist);
    }
}
