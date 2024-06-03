using AutoMapper;
using BackendTeamwork.Abstractions;
using BackendTeamwork.Databases;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using BackendTeamwork.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendTeamwork.Services
{
    public class StockService : IStockService
    {

        private IStockRepository _stockRepository;
        private IStockImageRepository _stockImageRepository;
        private DatabaseContext _databaseContext;
        private IMapper _mapper;

        public StockService(IStockRepository stockRepository, IMapper mapper, IStockImageRepository stockImageRepository, DatabaseContext databaseContext)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
            _stockImageRepository = stockImageRepository;
            _databaseContext = databaseContext;
        }

        public IEnumerable<StockJoinManyDto> FindMany(Guid userId, int limit, int offset)
        {
            return _stockRepository.FindMany(userId, limit, offset);
        }

        public IEnumerable<StockReadDto> FindMany(Guid productId)
        {
            return _stockRepository.FindMany(productId).Select(_mapper.Map<StockReadDto>);
        }

        public async Task<StockReadDto?> FindOne(Guid stockId)
        {
            return _mapper.Map<StockReadDto>(await _stockRepository.FindOne(stockId));
        }

        public async Task<StockReadDto> CreateOne(StockCreateDto newStock)

        {
            using (var transaction = _databaseContext.Database.BeginTransaction())
            {
                try
                {
                    StockReadDto createdStock = _mapper.Map<StockReadDto>(await _stockRepository.CreateOne(_mapper.Map<Stock>(newStock)));

                    IEnumerable<StockImageCreateDto> imgs = newStock.Images.Select(_mapper.Map<StockImageCreateDto>);

                    foreach (StockImageCreateDto img in imgs)
                    {
                        img.StockId = createdStock.Id;
                        img.Color = createdStock.Color;
                        img.Size = createdStock.Size;
                        await _stockImageRepository.CreateOne(_mapper.Map<StockImage>(img));
                    }
                    transaction.Commit();
                    return createdStock;
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                    throw;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new Exception("Something went wrong");
                }
            }
        }

        public async Task<StockReadDto?> UpdateOne(Guid stockId, StockUpdateDto updatedStock)
        {
            Stock? targetStock = await _stockRepository.FindOne(stockId);
            if (targetStock is null)
            {
                return null;
            }
            Stock stock = _mapper.Map<Stock>(updatedStock);
            stock.Id = stockId;
            return _mapper.Map<StockReadDto>(await _stockRepository.UpdateOne(stock));
        }

        public async Task<Stock?> DeleteOne(Guid stockId)
        {
            Stock? stock = await _stockRepository.FindOne(stockId);
            if (stock is not null)
            {
                return await _stockRepository.DeleteOne(stock);
            }
            return null;
        }

        public async Task<Stock> ReduceOne(OrderStockReduceDto orderStock)
        {
            return await _stockRepository.ReduceOne(orderStock);
        }

    }
}