using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using Microsoft.AspNetCore.Authorization;
using BackendTeamwork.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BackendTeamwork.Controllers
{
    public class OrdersController : BaseController
    {

        private IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<OrderReadDto>> FindManyByUserId(Guid userId, [FromQuery(Name = "sort")] SortBy sortBy)
        {
            return Ok(_orderService.FindManyByUserId(userId, sortBy));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<OrderReadDto>> FindMany()
        {
            return Ok(_orderService.FindMany());
        }

        [HttpGet("{paymentId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderJoinDto>> FindOne(Guid paymentId)
        {
            return Ok(await _orderService.FindOne(paymentId));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderReadDto>> CreateOne([FromBody] OrderCreateDto newOrder)
        {
            return Ok(await _orderService.CreateOne(newOrder));
        }

    }
}