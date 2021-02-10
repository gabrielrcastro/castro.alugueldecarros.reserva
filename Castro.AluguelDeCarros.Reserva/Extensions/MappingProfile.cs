using AutoMapper;
using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using Castro.AluguelDeCarros.Reserva.Domain.Models;
using Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Castro.AluguelDeCarros.Reserva.API.Extensions
{
    public static class MappingProfile
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                //DbModel para Domain
                var veiculo = cfg.CreateMap<VeiculoDbModel, Veiculo>();
                veiculo.ForAllMembers(opt => opt.Ignore());
                veiculo.ConstructUsing(src => new Veiculo(src.Id, src.Placa, src.ModeloId, src.Ano, src.ValorHora, (CombustivelEnum)src.Combustivel, src.LimitePortaMalas, src.CategoriaId, src.DataCriacao));

                var modelo = cfg.CreateMap<ModeloDbModel, Modelo>();
                modelo.ForAllMembers(opt => opt.Ignore());
                modelo.ConstructUsing(src => new Modelo(src.Id, src.Nome, src.MarcaId, src.DataCriacao));

                var marca = cfg.CreateMap<MarcaDbModel, Marca>();
                marca.ForAllMembers(opt => opt.Ignore());
                marca.ConstructUsing(src => new Marca(src.Id, src.Nome, src.DataCriacao));

                var categoria = cfg.CreateMap<CategoriaDbModel, Categoria>();
                categoria.ForAllMembers(opt => opt.Ignore());
                categoria.ForCtorParam("id", opt => opt.MapFrom(src => src.Id));
                categoria.ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome));
                categoria.ForCtorParam("veiculos", opt => opt.MapFrom(src => src.Veiculos));

                //Domain para DbModel
                cfg.CreateMap<Veiculo, VeiculoDbModel>();
                cfg.CreateMap<Modelo, ModeloDbModel>();
                cfg.CreateMap<Marca, MarcaDbModel>();
                cfg.CreateMap<Categoria, CategoriaDbModel>();

                //Model (Controller) para Domain
                var veiculoModel = cfg.CreateMap<VeiculoModel, Veiculo>();
                veiculoModel.ForAllMembers(opt => opt.Ignore());
                veiculoModel.ConstructUsing(src => new Veiculo(null, src.Placa, src.ModeloId, new DateTime(src.Ano, 1, 1), src.ValorHora, src.TipoCombustivel, src.LimitePortaMalas, src.CategoriaId, null));

            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
