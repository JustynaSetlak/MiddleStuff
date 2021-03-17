using System.Linq;
using IntegrationTestsSample.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTestsSample.IntegrationTests
{
    public class ProductApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ProductContext>));

                services.Remove(descriptor);

                services.AddDbContext<ProductContext>(options =>
                {
                    options.UseInMemoryDatabase("Products");
                });

                InitData(services);
            });
        }

        public void InitData(IServiceCollection services)
        {
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ProductContext>();
                context.Database.EnsureCreated();

                context.Products.AddRange(
                    new Product
                    {
                        Code = "P001",
                        Name = "Product1"
                    },
                    new Product
                    {
                        Code = "P002",
                        Name = "Product2"
                    });

                context.SaveChanges();
            }
        }
    }
}
