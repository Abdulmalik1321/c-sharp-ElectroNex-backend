using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using BackendTeamwork.Enums;

namespace BackendTeamwork.Abstractions
{
    public interface IOrderService
    {
        public IEnumerable<OrderReadDto> FindManyByUserId(Guid userId, SortBy sortBy);
        public IEnumerable<OrderReadDto> FindMany();
        public Task<OrderJoinDto> FindOne(Guid paymentId);
        public Task<OrderReadDto> CreateOne(OrderCreateDto newOrder);
    }
}