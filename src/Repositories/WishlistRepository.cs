using AutoMapper;
using BackendTeamwork.Abstractions;
using BackendTeamwork.Databases;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace BackendTeamwork.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {

        private DbSet<Wishlist> _wishlists;
        private DbSet<ProductWishlist> _productWishlist;
        private DbSet<Product> _products;
        private IMapper _mapper;

        private DatabaseContext _databaseContext;

        public WishlistRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _wishlists = databaseContext.Wishlist;
            _products = databaseContext.Product;
            _productWishlist = databaseContext.ProductWishlist;
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public IEnumerable<Wishlist> FindMany(Guid userId, int limit, int offset)
        {
            IEnumerable<Wishlist> wishlists = _wishlists.Where(w => w.UserId == userId);
            if (limit == 0 && offset == 0)
            {
                return wishlists;
            }
            return wishlists.Skip(offset).Take(limit);
        }


        public async Task<WishlistReadJoinDto?> FindOne(Guid wishlistId)
        {
            IQueryable<WishlistReadJoinDto> query = from productWishlist in _productWishlist
                                                    where productWishlist.WishlistId == wishlistId
                                                    join wishlist in _wishlists on productWishlist.WishlistId equals wishlist.Id
                                                    join product in _products on productWishlist.ProductId equals product.Id
                                                    group product by wishlist.Name into g
                                                    select new WishlistReadJoinDto
                                                    {
                                                        WishlistName = g.Key,
                                                        Products = g.Select(product => _mapper.Map<ProductJoinDto>(product))
                                                    };


            return await query.FirstOrDefaultAsync();
        }

        public async Task<Wishlist?> FindOneNoJoin(Guid wishlistId)
        {

            return await _wishlists.AsNoTracking().FirstOrDefaultAsync(wishlist => wishlist.Id == wishlistId);
        }

        public async Task<Wishlist> AddOneProduct(Guid wishlistId, Guid productId)
        {

            ProductWishlist productWishlist = new()
            {
                ProductId = productId,
                WishlistId = wishlistId
            };

            await _productWishlist.AddAsync(productWishlist);
            await _databaseContext.SaveChangesAsync();

            return await _wishlists.FirstOrDefaultAsync(w => w.Id == wishlistId);
        }

        public async Task<Wishlist> CreateOne(Wishlist newWishlist)
        {
            await _wishlists.AddAsync(newWishlist);
            await _databaseContext.SaveChangesAsync();
            return newWishlist;
        }

        public async Task<Wishlist> UpdateOne(Wishlist updatedWishlist)
        {
            _wishlists.Update(updatedWishlist);
            await _databaseContext.SaveChangesAsync();
            return updatedWishlist;
        }

        public async Task<Wishlist> DeleteOne(Wishlist targetWishlist)
        {
            _wishlists.Remove(targetWishlist);
            await _databaseContext.SaveChangesAsync();
            return targetWishlist;
        }
    }
}
