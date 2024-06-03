using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Abstractions
{
    public interface IStockRepository
    {

        public IEnumerable<StockJoinManyDto> FindMany(Guid userId, int limit, int offset);

        public IEnumerable<Stock> FindMany(Guid productId);

        public Task<Stock?> FindOne(Guid stockId);

        public Task<Stock> CreateOne(Stock newStock);

        public Task<Stock> UpdateOne(Stock updatedStock);

        public Task<Stock> DeleteOne(Stock deleteStock);

        public Task<Stock> ReduceOne(OrderStockReduceDto orderStock);


    }
}