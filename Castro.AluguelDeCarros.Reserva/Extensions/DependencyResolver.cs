using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using Castro.AluguelDeCarros.Reserva.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Castro.AluguelDeCarros.Reserva.API.Extensions
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            RegisterServices(services);
            RegisterRepositories(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IMarcaService, MarcaService>();
            services.AddScoped<IModeloService, ModeloService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<IModeloRepository, ModeloRepository>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
        }
    }
}
