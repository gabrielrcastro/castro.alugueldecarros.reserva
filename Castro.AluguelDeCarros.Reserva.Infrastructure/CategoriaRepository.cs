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
    public class CategoriaRepository : BaseRepository<CategoriaDbModel>, ICategoriaRepository
    {
        private readonly IMapper _mapper;

        public CategoriaRepository(IDbConnection conexao, IMapper mapper) : base(conexao)
        {
            _mapper = mapper;
        }

        public async Task<Categoria> BuscarCategoria(Guid id)
        {
            return _mapper.Map<Categoria>(await base.Buscar(id));
        }

        public async Task<IEnumerable<Categoria>> BuscarTodasCategorias()
        {
            return _mapper.Map<List<Categoria>>(await base.BuscarTodos());
        }

        public async Task SalvarCategoria(Categoria categoria)
        {
            await base.Salvar(_mapper.Map<CategoriaDbModel>(categoria));
        }

        public async Task AtualizarCategoria(Categoria categoria)
        {
            await base.Atualizar(_mapper.Map<CategoriaDbModel>(categoria));
        }
    }
}
