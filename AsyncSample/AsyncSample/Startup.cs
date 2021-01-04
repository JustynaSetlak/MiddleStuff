using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncSample.Options;
using AsyncSample.Services;
using AsyncSample.Services.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Refit;

namespace AsyncSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<WeatherApiOptions>(Configuration.GetSection(nameof(WeatherApiOptions)));
            
            var weatherApiOptions = Configuration.GetSection(nameof(WeatherApiOptions)).Get<WeatherApiOptions>();

            services.AddRefitClient<IWeatherForecastApi>()
                    .ConfigureHttpClient(c => c.BaseAddress = new Uri(weatherApiOptions.BaseUrl));

            services.AddTransient<IWeatherForcecastService, WeatherForcecastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
