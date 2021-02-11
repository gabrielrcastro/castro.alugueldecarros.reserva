using AutoMapper;
using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure
{
    public class ReservaRepository : BaseRepository<ReservaDbModel>, IReservaRepository
    {
        private readonly IMemoryCache _cache;
        private readonly IDbConnection _conexao;
        private readonly IMapper _mapper;

        private const string prefixoChaveCacheCotacao = "cotacao-";

        public ReservaRepository(IDbConnection conexao, IMapper mapper, IMemoryCache cache) : base(conexao)
        {
            _cache = cache;
            _conexao = conexao;
            _mapper = mapper;
        }

        public async Task SalvarReserva(Domain.Reserva reserva)
        {
            await base.Salvar(_mapper.Map<ReservaDbModel>(reserva));
        }

        public Cotacao BuscarCotacaoNoCache(Guid id)
        {
            return _cache.Get<Cotacao>(string.Concat(prefixoChaveCacheCotacao, id));
        }

        public void SalvarCotacaoNoCache(Cotacao cotacao)
        {
            _cache.Set(string.Concat(prefixoChaveCacheCotacao, cotacao.Id), cotacao);
        }

        public async Task<IEnumerable<Domain.Reserva>> BuscarReservasPorCliente(Guid clienteId)
        {
            var query = @"SELECT id, veiculoId, valor, horas, clienteId, dataCriacao, dataAlteracao
                        FROM Reserva 
                        WHERE clienteId = @clienteId";

            var parameters = new DynamicParameters();
            parameters.Add("clienteId", clienteId);

            var reservasDb = await _conexao.QueryAsync<ReservaDbModel>(query, parameters);

            return _mapper.Map<List<Domain.Reserva>>(reservasDb);
        }
    }
}
