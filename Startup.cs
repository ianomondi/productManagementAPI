using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductManagement.API.Controllers.Config;
using ProductManagement.API.Domain.Repositories;
using ProductManagement.API.Domain.Services;
using ProductManagement.API.Extensions;
using ProductManagement.API.Persistence.Contexts;
using ProductManagement.API.Persistence.Repositories;
using ProductManagement.API.Services;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Persistence.Repositories;
using Supermarket.API.Services;

namespace ProductManagement.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddCustomSwagger();

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                // Adds a custom error response factory when ModelState is invalid
                options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            });

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("AppDbContext")));

            services.AddScoped<ICompositeProductRepository, CompositeProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISKURepository, SKURepository>();
            services.AddScoped<IVariantRepository, VariantRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<IVariantOptionRepository, VariantOptionRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICompositeProductService, CompositeProductService>();
            services.AddScoped<ISKUService, SKUService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductVariantService, ProductVariantService>();
            services.AddScoped<IVariantService, VariantService>();
            services.AddScoped<IVariantOptionService, VariantOptionService>();

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomSwagger();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}