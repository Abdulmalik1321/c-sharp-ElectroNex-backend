using BackendTeamwork.Abstractions;
using BackendTeamwork.Databases;
using BackendTeamwork.Entities;
using BackendTeamwork.DTOs;
using AutoMapper;

namespace BackendTeamwork.Controllers
{
    public class PaymentService : IPaymentService
    {
        private IMapper _mapper;
        private IOrderService _orderService;
        private IStockService _stockService;
        private IProductService _productService;
        private DatabaseContext _databaseContext;
        private IPaymentRepository _paymentRepository;
        private IOrderStockService _orderStockService;

        public PaymentService(IPaymentRepository PaymentRepository, IMapper mapper, IOrderService orderService, IOrderStockService orderStockService, IStockService stockService, DatabaseContext databaseContext, IProductService productService)
        {
            _mapper = mapper;
            _stockService = stockService;
            _orderService = orderService;
            _databaseContext = databaseContext;
            _paymentRepository = PaymentRepository;
            _orderStockService = orderStockService;
            _productService = productService;
        }
        public async Task<PaymentReadDto?> FindOne(Guid paymentId)
        {
            return _mapper.Map<PaymentReadDto>(await _paymentRepository.FindOne(paymentId));
        }


        public async Task<PaymentReadDto> CreateOne(PaymentCreateDto newPayment)
        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                try
                {
                    PaymentReadDto payment = _mapper.Map<PaymentReadDto>(await _paymentRepository.CreateOne(_mapper.Map<Payment>(newPayment)));

                    OrderCreateDto orderDetails = new OrderCreateDto
                    {
                        Status = "Pending",
                        PaymentId = payment.Id,
                        UserId = payment.UserId
                    };
                    OrderReadDto order = await _orderService.CreateOne(orderDetails);

                    IEnumerable<OrderStock> orderStock = newPayment.Items.Select(_mapper.Map<OrderStock>);

                    foreach (OrderStock item in orderStock)
                    {
                        item.OrderId = order.Id;
                        Stock stock = await _stockService.ReduceOne(_mapper.Map<OrderStockReduceDto>(item));
                        item.Price = stock.Price;
                        // await _productService.AddSale(stock.ProductId, item.Quantity);
                        await _orderStockService.CreateOne(item);
                    }

                    transaction.Commit();
                    return payment;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new Exception("Something went wrong");
                }
            }
        }
    }
}