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
        private readonly IMapper _mapper;

        public VeiculoService(IVeiculoRepository veiculoRepository, IMapper mapper)
        {
            _veiculoRepository = veiculoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Categoria>> BuscarVeiculosPorCategoria()
        {
            return await _veiculoRepository.BuscarTodosVeiculosPorCategoria();
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
                    await _veiculoRepository.AtualizarVeiculo(veiculo);
                }
            }
            else
            {
                veiculo = _mapper.Map<Veiculo>(model);
                await _veiculoRepository.SalvarVeiculo(_mapper.Map<Veiculo>(model));
            }

            return veiculo;
        }
    }
}
