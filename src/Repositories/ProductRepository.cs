
using AutoMapper;
using BackendTeamwork.Abstractions;
using BackendTeamwork.Databases;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using BackendTeamwork.Enums;
using BackendTeamwork.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BackendTeamwork.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private DbSet<Product> _products;
        private DbSet<Stock> _stocks;
        private DbSet<StockImage> _stockImages;
        private DbSet<Brand> _brands;
        private DbSet<Category> _categories;
        private DbSet<User> _users;

        private DatabaseContext _databaseContext;

        private IMapper _mapper;

        public ProductRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _stockImages = databaseContext.StockImage;
            _brands = databaseContext.Brand;
            _users = databaseContext.User;
            _categories = databaseContext.Category;
            _products = databaseContext.Product;
            _stocks = databaseContext.Stock;
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public IEnumerable<ProductJoinDto> FindMany(int limit, int offset, SortBy sortBy = SortBy.Ascending, string searchTerm = "", string categoryFiltersString = null, string brandFiltersString = null)
        {
            List<string> categoryFilters = categoryFiltersString != null ? categoryFiltersString.Split('-').ToList() : null;
            List<string> brandFilters = brandFiltersString != null ? brandFiltersString.Split('-').ToList() : null;

            IEnumerable<ProductJoinDto> query = from product in _products.Where(productToSearch =>
                productToSearch.Name.ToLower().Contains(searchTerm.ToLower()) ||
                productToSearch.Description.ToLower().Contains(searchTerm.ToLower()))
                                                join stock in _stocks on product.Id equals stock.ProductId
                                                join stockImage in _stockImages on stock.Id equals stockImage.StockId
                                                join brand in _brands on product.BrandId equals brand.Id
                                                join category in _categories on product.CategoryId equals category.Id
                                                where stockImage.IsMain && stock.Condition == "new" && (categoryFilters == null || categoryFilters.Contains(category.Name)) && (brandFilters == null || brandFilters.Contains(brand.Name))
                                                group new { product, stock, stockImage, brand, category } by product.Name into grouped
                                                select new ProductJoinDto
                                                {
                                                    Id = grouped.First().product.Id,
                                                    Name = grouped.Key,
                                                    Images = grouped.Select(x => new List<string> { x.stockImage.Url, x.stockImage.Color }).Distinct().ToList(),
                                                    Colors = grouped.Select(x => x.stockImage.Color).Distinct().ToList(),
                                                    Sizes = grouped.Select(x => x.stock.Size).Distinct().ToList(),
                                                    Status = grouped.First().product.Status,
                                                    CreatedAt = grouped.First().product.CreatedAt,
                                                    NumberOfSales = grouped.First().product.NumberOfSales,
                                                    Price = grouped.Min(x => x.stock.Price),
                                                    BrandName = grouped.First().brand.Name,
                                                    CategoryName = grouped.First().category.Name,
                                                    Description = grouped.First().product.Description
                                                };


            IEnumerable<ProductJoinDto> sortedProducts;
            if (sortBy == SortBy.Ascending)
            {
                sortedProducts = query.OrderBy(query => query.Price);
            }
            else
            {
                sortedProducts = query.OrderByDescending(query => query.Price);
            }
            if (limit == 0 && offset == 0)
            {
                return sortedProducts;
            }

            return sortedProducts.Skip(offset * limit).Take(limit);
        }



        public async Task<ProductJoinSingleDto?> FindOne(Guid productId)
        {
            IQueryable<ProductJoinSingleDto> query = from product in _products
                                                     where product.Id == productId
                                                     join stock in _stocks on product.Id equals stock.ProductId
                                                     join stockImage in _stockImages on stock.Id equals stockImage.StockId
                                                     join user in _users on stock.UserId equals user.Id
                                                     group new { product, stock, stockImage, user } by product.Name into grouped
                                                     select new ProductJoinSingleDto
                                                     {
                                                         Id = grouped.First().product.Id,
                                                         Name = grouped.Key,
                                                         Stocks = grouped.Select(x => new StockImageReadDto
                                                         {
                                                             StockId = x.stock.Id,
                                                             Url = x.stockImage.Url,
                                                             IsMain = x.stockImage.IsMain,
                                                             Color = x.stockImage.Color,
                                                             Size = x.stockImage.Size,
                                                             Price = x.stock.Price,
                                                             Quantity = x.stock.Quantity,
                                                             Condition = x.stock.Condition,
                                                             UserName = $"{x.user.FirstName} {x.user.LastName}",
                                                         }),
                                                         Colors = grouped.Select(x => x.stock.Color).Distinct(),
                                                         Sizes = grouped.Select(x => x.stock.Size).Distinct(),
                                                         Description = grouped.First().product.Description,
                                                     };

            return await query.FirstOrDefaultAsync();

            // return await _products.AsNoTracking().FirstOrDefaultAsync(product => product.Id == productId);
        }
        public async Task<Product?> FindOneNoJoin(Guid productId)
        {
            return await _products.AsNoTracking().FirstOrDefaultAsync(product => product.Id == productId);
        }

        public async Task<Product> AddSale(Guid productId, int quantity)
        {
            Product? product = await this.FindOneNoJoin(productId);

            if (product is null) throw new Exception("product not found");

            product.NumberOfSales += quantity;

            _products.Update(product);
            await _databaseContext.SaveChangesAsync();

            return product;

        }

        public async Task<Product> CreateOne(Product newProduct)
        {


            await _products.AddAsync(newProduct);
            await _databaseContext.SaveChangesAsync();
            return newProduct;
        }

        public async Task<Product> UpdateOne(Product updatedProduct)
        {
            _products.Update(updatedProduct);
            await _databaseContext.SaveChangesAsync();
            return updatedProduct;
        }

        public async Task<Product> DeleteOne(Product targetProduct)
        {
            _products.Remove(targetProduct);
            await _databaseContext.SaveChangesAsync();
            return targetProduct;
        }

        public IEnumerable<Product> Search(string searchTerm)
        {
            return _products.Where(product =>
                product.Name.ToLower().Contains(searchTerm.ToLower()) ||
                product.Description.ToLower().Contains(searchTerm.ToLower())
            );
        }

    }
}