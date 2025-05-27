using Application.Handlers;
using Domain.Context;
using Infrastructure.Contracts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddResponseCompression();

            // Add services to the container.

            builder.Services.AddControllers();

            // Add Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //register services
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();


            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllProductsHandler).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductHandler).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteProductHandler).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetProductByIdHandler).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateProductHandler).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetBestSellingProductsReportHandler).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderHandler).Assembly));
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetTopSpendingUsersHandler).Assembly));


            builder.Services.AddDbContext<OMDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


           
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
          
            //Use swagger in development mode

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseResponseCompression();

            app.MapControllers();

            app.Run();
        }
    }
}
