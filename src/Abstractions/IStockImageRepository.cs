using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Abstractions
{
    public interface IStockImageRepository
    {

        public IEnumerable<StockImage> FindMany(int limit, int offset);
        public Task<StockImage?> FindOne(Guid id);
        public Task<StockImage> CreateOne(StockImage newStockImage);

        public Task<StockImage> UpdateOne(StockImage updateStockImage);

        public Task<StockImage> DeleteOne(StockImage deletedStockImage);

    }
}