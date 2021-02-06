using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly IConfiguration _config;
        private readonly IMemoryCache _cache;
        private const string prefixoChaveCacheCotacao = "cotacao-";

        public ReservaRepository(IConfiguration configuration, IMemoryCache cache)
        {
            _config = configuration;
            _cache = cache;
        }

        public async Task SalvarReserva(Domain.Reserva reserva)
        {
            using (SqlConnection conexao = new SqlConnection(_config.GetConnectionString("default")))
            {
                var reservaDbModel = new ReservaDbModel
                {
                    Id = reserva.Id,
                    VeiculoId = reserva.VeiculoId,
                    UsuarioId = reserva.ClienteId,
                    Horas = reserva.TotalHoras,
                    Valor = reserva.ValorTotal,
                    DataAlteracao = reserva.DataAlteracao,
                    DataCriacao = reserva.DataCriacao
                };

                await conexao.InsertAsync(reservaDbModel);
            }
        }

        public Cotacao BuscarCotacaoNoCache(Guid id)
        {
            return _cache.Get<Cotacao>(string.Concat(prefixoChaveCacheCotacao, id));
        }

        public void SalvarCotacaoNoCache(Cotacao cotacao)
        {
            _cache.Set(string.Concat(prefixoChaveCacheCotacao, cotacao.Id), cotacao);
        }
    }
}
