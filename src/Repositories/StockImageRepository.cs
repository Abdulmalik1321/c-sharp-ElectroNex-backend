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
    public class StockImageRepository : IStockImageRepository
    {

        private DbSet<StockImage> _stockImage;
        private DatabaseContext _databaseContext;

        public StockImageRepository(DatabaseContext databaseContext)
        {
            _stockImage = databaseContext.StockImage;
            _databaseContext = databaseContext;

        }


        public IEnumerable<StockImage> FindMany(int limit, int offset)
        {
            return _stockImage.Skip(offset).Take(limit).ToList();
        }

        public async Task<StockImage?> FindOne(Guid id)
        {
            return await _stockImage.AsNoTracking().FirstOrDefaultAsync(stockImage => stockImage.Id == id);
        }

        public async Task<StockImage> CreateOne(StockImage newStockImage)
        {
            await _stockImage.AddAsync(newStockImage);
            await _databaseContext.SaveChangesAsync();
            return newStockImage;
        }

        public async Task<StockImage> UpdateOne(StockImage updateStockImage)
        {
            _stockImage.Update(updateStockImage);
            await _databaseContext.SaveChangesAsync();
            return updateStockImage;
        }

        public async Task<StockImage> DeleteOne(StockImage deletedStockImage)
        {
            _stockImage.Remove(deletedStockImage);
            await _databaseContext.SaveChangesAsync();
            return deletedStockImage;
        }

    }
}