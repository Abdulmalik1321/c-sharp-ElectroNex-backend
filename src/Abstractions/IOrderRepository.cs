using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using BackendTeamwork.Enums;

namespace BackendTeamwork.Abstractions
{
    public interface IOrderRepository
    {
        public IEnumerable<Order> FindManyByUserId(Guid userId, SortBy sortBy);
        public IEnumerable<Order> FindMany();
        public Task<OrderJoinDto> FindOne(Guid paymentId);
        public Task<Order> CreateOne(Order newOrder);

    }
}