using AutoMapper;
using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure
{
    public class MarcaRepository : BaseRepository<MarcaDbModel>, IMarcaRepository
    {
        private readonly IMapper _mapper;

        public MarcaRepository(IDbConnection conexao, IMapper mapper) : base(conexao)
        {
            _mapper = mapper;
        }

        public async Task<Marca> BuscarMarca(Guid id)
        {
            return _mapper.Map<Marca>(await base.Buscar(id));
        }

        public async Task<IEnumerable<Marca>> BuscarTodasMarcas()
        {
            return _mapper.Map<List<Marca>>(await base.BuscarTodos());
        }

        public async Task SalvarMarca(Marca marca)
        {
            await base.Salvar(_mapper.Map<MarcaDbModel>(marca));
        }

        public async Task AtualizarMarca(Marca marca)
        {
            await base.Salvar(_mapper.Map<MarcaDbModel>(marca));
        }
    }
}
