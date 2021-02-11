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
    public class ModeloService : IModeloService
    {
        private readonly IModeloRepository _modeloRepository;
        private readonly IMarcaService _marcaService;
        private readonly IMapper _mapper;

        public ModeloService(IModeloRepository modeloRepository, IMarcaService marcaService, IMapper mapper)
        {
            _modeloRepository = modeloRepository;
            _marcaService = marcaService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Modelo>> BuscarTodos()
        {
            return await _modeloRepository.BuscarTodosModelos();
        }

        public async Task<Modelo> Obter(Guid id)
        {
            return await _modeloRepository.BuscarModelo(id);
        }

        public async Task<Modelo> Salvar(Guid? id, ModeloModel model)
        {
            Modelo modelo;

            if (id.HasValue)
            {
                modelo = await _modeloRepository.BuscarModelo(id.Value);

                if (modelo != null && modelo.Id != Guid.Empty)
                {
                    modelo.DefinirOuAlterarNome(model.Nome);
                    modelo.ValidarMarca(await _marcaService.Obter(model.MarcaId));

                    if (modelo.Valido)
                        await _modeloRepository.AtualizarModelo(modelo);
                }
            }
            else
            {
                modelo = _mapper.Map<Modelo>(model);
                modelo.ValidarMarca(await _marcaService.Obter(model.MarcaId));

                if (modelo.Valido)
                    await _modeloRepository.SalvarModelo(modelo);
            }

            return modelo;
        }
    }
}
