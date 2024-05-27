using System.Reflection;
using System.Text;
using BackendTeamwork.Abstractions;
using BackendTeamwork.Controllers;
using BackendTeamwork.Databases;
using BackendTeamwork.Repositories;
using BackendTeamwork.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BackendTeamwork.Middlewares;
using Npgsql;
using BackendTeamwork.Enums;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
// builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// configuring DB
var _config = builder.Configuration;
var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config["DB"]);
dataSourceBuilder.MapEnum<Role>();

var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<DatabaseContext>((options) =>
{
    options.UseNpgsql(dataSource).UseSnakeCaseNamingConvention();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);



// dependency injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();

builder.Services.AddScoped<IStockImageRepository, StockImageRepository>();

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockService, StockService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();

builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IWishlistService, WishlistService>();

builder.Services.AddScoped<IShippingRepository, ShippingRepository>();
builder.Services.AddScoped<IShippingService, ShippingService>();

builder.Services.AddScoped<IOrderStockRepository, OrderStockRepository>();
builder.Services.AddScoped<IOrderStockService, OrderStockService>();

builder.Services.AddScoped<IShippingRepository, ShippingRepository>();
builder.Services.AddScoped<IShippingService, ShippingService>();

// middleware to handle exceptions
builder.Services.AddScoped<CustomErrorMiddleware>();



var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration["Cors_Origin"]!)
                          .AllowAnyHeader()
                            .AllowAnyMethod(); ;
                      });
});






builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt_Issuer"],
                        ValidAudience = builder.Configuration["Jwt_Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt_SigningKey"]!))
                    };
                });





var app = builder.Build();

app.UseMiddleware<CustomErrorMiddleware>();
// Should be added
app.MapControllers();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();


app.Run();