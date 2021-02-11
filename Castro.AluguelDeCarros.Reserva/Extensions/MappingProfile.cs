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
                modelo.ForCtorParam("id", opt => opt.MapFrom(src => src.Id));
                modelo.ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome));
                modelo.ForCtorParam("marcaId", opt => opt.MapFrom(src => src.MarcaId));
                modelo.ForCtorParam("veiculos", opt => opt.MapFrom(src => src.Veiculos));
                modelo.ForCtorParam("dataCriacao", opt => opt.MapFrom(src => src.DataCriacao));

                var marca = cfg.CreateMap<MarcaDbModel, Marca>();
                marca.ForAllMembers(opt => opt.Ignore());
                marca.ForCtorParam("id", opt => opt.MapFrom(src => src.Id));
                marca.ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome));
                marca.ForCtorParam("veiculos", opt => opt.MapFrom(src => src.Veiculos));
                marca.ForCtorParam("dataCriacao", opt => opt.MapFrom(src => src.DataCriacao));

                var categoria = cfg.CreateMap<CategoriaDbModel, Categoria>();
                categoria.ForAllMembers(opt => opt.Ignore());
                categoria.ForCtorParam("id", opt => opt.MapFrom(src => src.Id));
                categoria.ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome));
                categoria.ForCtorParam("veiculos", opt => opt.MapFrom(src => src.Veiculos));
                categoria.ForCtorParam("dataCriacao", opt => opt.MapFrom(src => src.DataCriacao));

                var reserva = cfg.CreateMap<ReservaDbModel, Domain.Reserva>();
                reserva.ForAllMembers(opt => opt.Ignore());
                reserva.ForCtorParam("id", opt => opt.MapFrom(src => src.Id));
                reserva.ForCtorParam("veiculoId", opt => opt.MapFrom(src => src.VeiculoId));
                reserva.ForCtorParam("totalHoras", opt => opt.MapFrom(src => src.Horas));
                reserva.ForCtorParam("valorTotal", opt => opt.MapFrom(src => src.Valor));
                reserva.ForCtorParam("clienteId", opt => opt.MapFrom(src => src.ClienteId));
                reserva.ForCtorParam("dataCriacao", opt => opt.MapFrom(src => src.DataCriacao));

                var cliente = cfg.CreateMap<UsuarioDbModel, Cliente>();
                cliente.ForAllMembers(opt => opt.Ignore());
                cliente.ForCtorParam("id", opt => opt.MapFrom(src => src.Id));
                cliente.ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome));
                cliente.ForCtorParam("cpf", opt => opt.MapFrom(src => src.Cpf));
                cliente.ForCtorParam("dataDeNascimento", opt => opt.MapFrom(src => src.DataNascimento));
                cliente.ForCtorParam("enderecoCep", opt => opt.MapFrom(src => src.EnderecoCep));
                cliente.ForCtorParam("enderecoLogradouro", opt => opt.MapFrom(src => src.EnderecoLogradouro));
                cliente.ForCtorParam("enderecoNumero", opt => opt.MapFrom(src => src.EnderecoNumero));
                cliente.ForCtorParam("enderecoComplemento", opt => opt.MapFrom(src => src.EnderecoComplemento));
                cliente.ForCtorParam("enderecoCidade", opt => opt.MapFrom(src => src.EnderecoCidade));
                cliente.ForCtorParam("enderecoEstado", opt => opt.MapFrom(src => src.EnderecoEstado));
                cliente.ForCtorParam("dataCriacao", opt => opt.MapFrom(src => src.DataCriacao));

                var operador = cfg.CreateMap<UsuarioDbModel, Operador>();
                operador.ForAllMembers(opt => opt.Ignore());
                operador.ForCtorParam("id", opt => opt.MapFrom(src => src.Id));
                operador.ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome));
                operador.ForCtorParam("matricula", opt => opt.MapFrom(src => src.Matricula));
                operador.ForCtorParam("dataCriacao", opt => opt.MapFrom(src => src.DataCriacao));

                //Domain para DbModel
                cfg.CreateMap<Veiculo, VeiculoDbModel>();
                cfg.CreateMap<Modelo, ModeloDbModel>();
                cfg.CreateMap<Marca, MarcaDbModel>();
                cfg.CreateMap<Categoria, CategoriaDbModel>();
                cfg.CreateMap<Domain.Reserva, ReservaDbModel>();
                cfg.CreateMap<Cliente, UsuarioDbModel>();
                cfg.CreateMap<Operador, UsuarioDbModel>();
                cfg.CreateMap<UsuarioBase, UsuarioDbModel>();

                //Model (Controller) para Domain
                var veiculoModel = cfg.CreateMap<VeiculoModel, Veiculo>();
                veiculoModel.ForAllMembers(opt => opt.Ignore());
                veiculoModel.ConstructUsing(src => new Veiculo(null, src.Placa, src.ModeloId, new DateTime(src.Ano, 1, 1), src.ValorHora, src.TipoCombustivel, src.LimitePortaMalas, src.CategoriaId, null));

                var categoriaModel = cfg.CreateMap<string, Categoria>();
                categoriaModel.ConstructUsing(src => new Categoria(null, src, null));

                var marcaModel = cfg.CreateMap<string, Marca>();
                marcaModel.ConstructUsing(src => new Marca(null, src, null));

                var modeloModel = cfg.CreateMap<ModeloModel, Modelo>();
                modeloModel.ConstructUsing(src => new Modelo(null, src.Nome, src.MarcaId, null));

                var usuarioModel = cfg.CreateMap<CadastrarClienteModel, Cliente>();
                usuarioModel.ForAllMembers(opt => opt.Ignore());
                usuarioModel.ConstructUsing(src => new Cliente(null, src.Nome, src.Cpf, src.Senha, src.DataNascimento, src.Endereco.Cep, src.Endereco.Logradouro, src.Endereco.Numero, src.Endereco.Complemento, src.Endereco.Cidade, src.Endereco.Estado, null));

            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
