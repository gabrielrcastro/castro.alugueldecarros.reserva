using AutoMapper;
using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Domain.Models;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly ICategoriaService _categoriaService;
        private readonly IModeloService _modeloService;
        private readonly IMapper _mapper;

        public VeiculoService(IVeiculoRepository veiculoRepository, ICategoriaService categoriaService, IModeloService modeloService, IMapper mapper)
        {
            _veiculoRepository = veiculoRepository;
            _categoriaService = categoriaService;
            _modeloService = modeloService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Veiculo>> BuscarTodos()
        {
            return await _veiculoRepository.BuscarTodosVeiculos();
        }

        public async Task<Veiculo> Obter(Guid id)
        {
            return await _veiculoRepository.BuscarVeiculo(id);
        }

        public async Task<Veiculo> Salvar(Guid? id, VeiculoModel model)
        {
            Veiculo veiculo;

            if (id.HasValue)
            {
                veiculo = await _veiculoRepository.BuscarVeiculo(id.Value);

                if (veiculo != null && veiculo.Id != Guid.Empty)
                {
                    veiculo.Alterar(model.ValorHora, model.CategoriaId);
                    veiculo.ValidarCategoria(await _categoriaService.Obter(model.CategoriaId));
                    veiculo.ValidarModelo(await _modeloService.Obter(model.ModeloId));

                    if (veiculo.Valido)
                        await _veiculoRepository.AtualizarVeiculo(veiculo);
                }
            }
            else
            {
                veiculo = _mapper.Map<Veiculo>(model);
                veiculo.ValidarCategoria(await _categoriaService.Obter(veiculo.CategoriaId));
                veiculo.ValidarModelo(await _modeloService.Obter(veiculo.ModeloId));

                if (veiculo.Valido)
                    await _veiculoRepository.SalvarVeiculo(veiculo);
            }

            return veiculo;
        }

        public async Task<IEnumerable<Categoria>> BuscarVeiculosPorCategoria()
        {
            return await _veiculoRepository.BuscarTodosVeiculosPorCategoria();
        }

        public async Task<IEnumerable<Marca>> BuscarVeiculosPorMarca(Guid id)
        {
            return await _veiculoRepository.BuscarVeiculosPorMarca(id);
        }

        public async Task<IEnumerable<Modelo>> BuscarVeiculosPorModelo(Guid id)
        {
            return await _veiculoRepository.BuscarVeiculosPorModelo(id);
        }
    }
}
