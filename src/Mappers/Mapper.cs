using AutoMapper;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
namespace BackendTeamwork.Mappers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserReadDto, User>();

            CreateMap<AddressCreateDto, Address>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<BrandCreateDto, Brand>();
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderStockCreateDto, OrderStock>();
            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ReviewCreateDto, Review>();
            CreateMap<StockCreateDto, Stock>();
            CreateMap<UserCreateDto, User>();
            CreateMap<WishlistCreateDto, Wishlist>();
            CreateMap<ShippingCreateDto, Shipping>();

            CreateMap<AddressUpdateDto, Address>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<BrandUpdateDto, Brand>();
            CreateMap<OrderUpdateDto, Order>();
            // CreateMap<OrderStockUpdateDto, OrderStock>();
            // CreateMap<PaymentUpdateDto, Payment>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<ReviewUpdateDto, Review>();
            CreateMap<StockUpdateDto, Stock>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<WishlistUpdateDto, Wishlist>();
            CreateMap<ShippingUpdateDto, Shipping>();

            CreateMap<Address, AddressReadDto>();
            CreateMap<Category, CategoryReadDto>();
            CreateMap<Brand, BrandReadDto>();
            CreateMap<Brand, BrandReadDto>();
            CreateMap<Order, OrderReadDto>();
            CreateMap<OrderStock, OrderStockReadDto>();
            CreateMap<Payment, PaymentReadDto>();
            CreateMap<Product, ProductReadDto>();
            CreateMap<Review, ReviewReadDto>();
            CreateMap<Stock, StockReadDto>();
            CreateMap<User, UserReadDto>();
            CreateMap<Wishlist, WishlistReadDto>();
            CreateMap<Shipping, ShippingReadDto>();


            CreateMap<OrderStockReduceDto, OrderStockCreateDto>();
            CreateMap<OrderStockReduceDto, OrderStock>();
            CreateMap<OrderStock, OrderStockReduceDto>();

            CreateMap<StockCreateDto, StockCreateDtoWithoutId>();
            CreateMap<StockCreateDtoWithoutId, StockCreateDto>();

            CreateMap<StockCreateDto, Stock>();
            CreateMap<Stock, StockCreateDto>();

            CreateMap<ProductJoinSingleDto, Product>();
            CreateMap<Product, ProductJoinSingleDto>();

            CreateMap<ProductJoinSingleDto, ProductReadDto>();
            CreateMap<ProductReadDto, ProductJoinSingleDto>();

            CreateMap<ProductJoinSingleDto, Product>();
            CreateMap<Product, ProductJoinSingleDto>();

            CreateMap<StockImage, StockImageReadDto>();
            CreateMap<StockImageReadDto, StockImage>();

            CreateMap<StockImageCreateDto, StockImageWithoutIdDto>();
            CreateMap<StockImageCreateDto, StockImage>();
            CreateMap<StockImage, StockImageCreateDto>();
            CreateMap<StockImageWithoutIdDto, StockImageCreateDto>();

            CreateMap<StockImageCreateDto, StockImageWithoutIdDto>();
            CreateMap<StockImageWithoutIdDto, StockImageCreateDto>();

            CreateMap<ProductJoinDto, Product>();
            CreateMap<Product, ProductJoinDto>();

        }
    }
}