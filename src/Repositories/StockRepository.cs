using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTeamwork.Abstractions;
using BackendTeamwork.Databases;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTeamwork.Repositories
{
    public class StockRepository : IStockRepository
    {

        private DbSet<Stock> _stocks;
        private DbSet<StockImage> _stockImages;
        private DbSet<Product> _products;
        private DbSet<User> _users;
        private DatabaseContext _databaseContext;

        public StockRepository(DatabaseContext databaseContext)
        {
            _stocks = databaseContext.Stock;
            _users = databaseContext.User;
            _stockImages = databaseContext.StockImage;
            _products = databaseContext.Product;
            _databaseContext = databaseContext;

        }

        public IEnumerable<StockJoinManyDto> FindMany(Guid userId, int limit, int offset)
        {
            IEnumerable<StockJoinManyDto> query = from stock in _stocks.Where(stock => stock.UserId == userId)
                                                  join stockImage in _stockImages on stock.Id equals stockImage.StockId
                                                  join product in _products on stock.ProductId equals product.Id
                                                  join user in _users on stock.UserId equals user.Id
                                                  group new { stock, product, stockImage, user } by stock.Id into grouped
                                                  select new StockJoinManyDto
                                                  {
                                                      Id = grouped.First().stock.Id,
                                                      ProductName = grouped.First().product.Name,
                                                      Images = grouped
                                                                .OrderByDescending(x => x.stockImage.IsMain)
                                                                .Select(x => new StockImagesDto
                                                                {
                                                                    Url = x.stockImage.Url,
                                                                    IsMain = x.stockImage.IsMain,
                                                                }),

                                                      Color = grouped.First().stock.Color,
                                                      Size = grouped.First().stock.Size,
                                                      Price = grouped.First().stock.Price,
                                                      Quantity = grouped.First().stock.Quantity,
                                                      UserName = $"{grouped.First().user.FirstName} {grouped.First().user.LastName}",
                                                      Condition = grouped.First().stock.Condition
                                                  };

            if (limit == 0 && offset == 0)
            {
                return query;
            }

            return query.Skip(offset).Take(limit);
        }


        public IEnumerable<Stock> FindMany(Guid productId)
        {
            IEnumerable<Stock> targetStocks = _stocks.Where(stock => stock.ProductId == productId);
            return targetStocks;
        }

        public async Task<Stock?> FindOne(Guid stockId)
        {
            return await _stocks.AsNoTracking().FirstOrDefaultAsync(stock => stock.Id == stockId);
        }


        public async Task<Stock> CreateOne(Stock newStock)
        {
            await _stocks.AddAsync(newStock);
            await _databaseContext.SaveChangesAsync();
            return newStock;
        }


        public async Task<Stock> UpdateOne(Stock updatedStock)
        {
            Console.WriteLine($"{updatedStock}");

            _stocks.Update(updatedStock);
            await _databaseContext.SaveChangesAsync();
            return updatedStock;
        }


        public async Task<Stock> DeleteOne(Stock deleteStock)
        {
            _stocks.Remove(deleteStock);
            await _databaseContext.SaveChangesAsync();
            return deleteStock;
        }

        public async Task<Stock> ReduceOne(OrderStockReduceDto orderStock)
        {
            Stock stock = _stocks.AsNoTracking().First(stock => stock.Id == orderStock.StockId);

            if (stock.Quantity - orderStock.Quantity < 0)
            {
                throw new Exception("Low quantity");
            }
            else
            {
                stock.Quantity -= orderStock.Quantity;
            }

            _stocks.Update(stock);
            await _databaseContext.SaveChangesAsync();
            return stock;
        }
    }
}