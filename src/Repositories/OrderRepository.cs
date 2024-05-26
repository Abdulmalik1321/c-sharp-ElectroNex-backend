using BackendTeamwork.Entities;
using BackendTeamwork.Databases;
using BackendTeamwork.Abstractions;
using Microsoft.EntityFrameworkCore;
using BackendTeamwork.Enums;
using BackendTeamwork.DTOs;

namespace BackendTeamwork.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private DbSet<Order> _orders;
        private DbSet<Stock> _stocks;
        private DbSet<StockImage> _stockImages;
        private DbSet<Product> _products;
        private DbSet<User> _users;
        private DbSet<OrderStock> _orderStocks;
        private DatabaseContext _databaseContext;

        public OrderRepository(DatabaseContext databaseContext)
        {
            _orders = databaseContext.Order;
            _stocks = databaseContext.Stock;
            _products = databaseContext.Product;
            _users = databaseContext.User;
            _orderStocks = databaseContext.OrderStock;
            _stockImages = databaseContext.StockImage;
            _databaseContext = databaseContext;

        }

        public IEnumerable<Order> FindManyByUserId(Guid userId, SortBy sortBy = SortBy.Ascending)
        {
            if (sortBy == SortBy.Ascending)
            {
                return _orders.Where(order => order.UserId == userId).OrderBy(order => order.Date);
            }
            else
            {
                return _orders.Where(order => order.UserId == userId).OrderByDescending(order => order.Date);
            }
            // return _orders.Where(order => order.UserId == userId);
        }

        public IEnumerable<Order> FindMany()
        {
            return _orders;
        }

        public async Task<OrderJoinDto> FindOne(Guid paymentId)
        {
            IQueryable<OrderJoinDto> query = from order in _orders
                                             where order.PaymentId == paymentId
                                             join orderStock in _orderStocks on order.Id equals orderStock.OrderId
                                             join stock in _stocks on orderStock.StockId equals stock.Id
                                             join stockImage in _stockImages on stock.Id equals stockImage.StockId
                                             join product in _products on stock.ProductId equals product.Id
                                             join user in _users on stock.UserId equals user.Id
                                             where stockImage.IsMain
                                             group new { order, orderStock, stock, stockImage, product, user } by order.Id into grouped
                                             select new OrderJoinDto
                                             {
                                                 Id = grouped.Key,
                                                 Status = grouped.First().order.Status,
                                                 Date = grouped.First().order.Date,
                                                 Stocks = grouped.Select(x => new StockReadWithImgDto
                                                 {
                                                     StockId = x.stock.Id,
                                                     Price = x.stock.Price,
                                                     Quantity = x.orderStock.Quantity,
                                                     Size = x.stock.Size,
                                                     Color = x.stock.Color,
                                                     Condition = x.stock.Condition,
                                                     Image = x.stockImage.Url, // assuming StockImage has a Url property
                                                     ProductName = x.product.Name, // assuming Product has a Name property
                                                     UserName = $"{x.user.FirstName} {x.user.LastName}", // assuming User has FirstName and LastName properties
                                                 }),
                                             };

            return await query.FirstOrDefaultAsync();
            // return await _orders.AsNoTracking().FirstOrDefaultAsync(order => order.PaymentId == paymentId);
        }

        public async Task<Order> CreateOne(Order newOrder)
        {
            await _orders.AddAsync(newOrder);
            await _databaseContext.SaveChangesAsync();
            return newOrder;
        }
    }
}