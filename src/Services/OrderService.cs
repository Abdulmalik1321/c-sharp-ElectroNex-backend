using AutoMapper;
using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using BackendTeamwork.Enums;
using BackendTeamwork.Repositories;

namespace BackendTeamwork.Services
{
    public class OrderService : IOrderService
    {

        private IOrderRepository _orderRepository;
        private IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public IEnumerable<OrderReadDto> FindManyByUserId(Guid userId, SortBy sortBy)
        {
            return _orderRepository.FindManyByUserId(userId, sortBy).Select(_mapper.Map<OrderReadDto>);
        }

        public IEnumerable<OrderReadDto> FindMany()
        {
            return _orderRepository.FindMany().Select(_mapper.Map<OrderReadDto>);
        }

        public async Task<OrderJoinDto> FindOne(Guid paymentId)
        {
            return await _orderRepository.FindOne(paymentId);
        }

        public async Task<OrderReadDto> CreateOne(OrderCreateDto newOrder)
        {
            return _mapper.Map<OrderReadDto>(await _orderRepository.CreateOne(_mapper.Map<Order>(newOrder)));
        }
    }
}