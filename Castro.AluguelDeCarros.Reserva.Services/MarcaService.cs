using AutoMapper;
using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _marcaRepository;
        private readonly IMapper _mapper;

        public MarcaService(IMarcaRepository marcaRepository, IMapper mapper)
        {
            _marcaRepository = marcaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Marca>> BuscarTodas()
        {
            return await _marcaRepository.BuscarTodasMarcas();
        }

        public async Task<Marca> Obter(Guid id)
        {
            return await _marcaRepository.BuscarMarca(id);
        }

        public async Task<Marca> Salvar(Guid? id, string nome)
        {
            Marca marca;

            if (id.HasValue)
            {
                marca = await _marcaRepository.BuscarMarca(id.Value);

                if (marca != null && marca.Id != Guid.Empty)
                {
                    marca.DefinirOuAlterarNome(nome);
                    if (marca.Valido)
                        await _marcaRepository.AtualizarMarca(marca);
                }
            }
            else
            {
                marca = _mapper.Map<Marca>(nome);
                await _marcaRepository.SalvarMarca(marca);
            }

            return marca;
        }
    }
}
