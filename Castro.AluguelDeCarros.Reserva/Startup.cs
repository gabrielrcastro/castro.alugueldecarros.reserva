using Castro.AluguelDeCarros.Reserva.API.Extensions;
using Castro.AluguelDeCarros.Reserva.Services;
using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Castro.AluguelDeCarros.Reserva.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(new Filters.DefaultExceptionFilterAttribute()))
            .AddJsonOptions(options => { options.JsonSerializerOptions.IgnoreNullValues = true; });

            services.AddControllers();

            var producerConfig = new ProducerConfig();
            var consumerConfig = new ConsumerConfig();
            Configuration.Bind("Producer", producerConfig);
            Configuration.Bind("Consumer", consumerConfig);
            services.AddSingleton(producerConfig);
            services.AddSingleton(consumerConfig);

            services.AddDependencyResolver();

            //services.AddSingleton<IHostedService, ReservaConsumidor>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Castro.AluguelDeCarros.Reserva",
                    Description = "Castro.AluguelDeCarros.Reserva",
                    Version = "v1"
                });

                var apiPath = Path.Combine(AppContext.BaseDirectory, "Castro.AluguelDeCarros.Reserva.xml");

                c.IncludeXmlComments(apiPath);
            });

            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Castro.AluguelDeCarros.Reserva");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
