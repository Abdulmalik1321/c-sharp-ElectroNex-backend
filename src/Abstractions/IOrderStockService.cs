using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Abstractions
{
    public interface IOrderStockService
    {
        public IEnumerable<OrderStock> FindMany(Guid orderId);
        public Task<OrderStock> CreateOne(OrderStock newOrderStock);
        public Task<IEnumerable<OrderStockReadDto



        >> CreateMany(IEnumerable<OrderStockCreateDto> newOrderStocks);
        // public void DeleteMany(Guid stockId);
        // public void DeleteOne(Guid id);
    }
}