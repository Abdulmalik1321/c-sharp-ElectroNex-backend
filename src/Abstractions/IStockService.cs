using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Abstractions
{
    public interface IStockService
    {

        public IEnumerable<StockReadDto> FindMany();
        public IEnumerable<StockReadDto> FindMany(Guid productId);
        public Task<StockReadDto?> FindOne(Guid stockId);

        public Task<StockReadDto> CreateOne(StockCreateDto newStock);

        public Task<StockReadDto?> UpdateOne(Guid stockId, StockUpdateDto updatedStock);

        public Task<Stock?> DeleteOne(Guid stockId);
    }
}
