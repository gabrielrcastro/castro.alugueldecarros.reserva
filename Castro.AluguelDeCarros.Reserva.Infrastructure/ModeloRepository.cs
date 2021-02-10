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
    public class ModeloRepository : BaseRepository<ModeloDbModel>, IModeloRepository
    {
        private readonly IMapper _mapper;

        public ModeloRepository(IDbConnection conexao, IMapper mapper) : base(conexao)
        {
            _mapper = mapper;
        }

        public async Task<Modelo> BuscarModelo(Guid id)
        {
            return _mapper.Map<Modelo>(await base.Buscar(id));
        }

        public async Task<IEnumerable<Modelo>> BuscarTodosModelos()
        {
            return _mapper.Map<List<Modelo>>(await base.BuscarTodos());
        }

        public async Task SalvarModelo(Modelo modelo)
        {
            await base.Salvar(_mapper.Map<ModeloDbModel>(modelo));
        }

        public async Task AtualizarModelo(Modelo modelo)
        {
            await base.Salvar(_mapper.Map<ModeloDbModel>(modelo));
        }
    }
}
