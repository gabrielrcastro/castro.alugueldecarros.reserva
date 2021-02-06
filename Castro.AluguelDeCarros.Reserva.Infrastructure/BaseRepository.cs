using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure
{
    public abstract class BaseRepository<TDbModel> where TDbModel : class
    {
        private readonly IDbConnection _conexao;

        public BaseRepository(IDbConnection conexao)
        {
            _conexao = conexao;
        }

        public async Task<TDbModel> Buscar(Guid id)
        {
            try
            {
                _conexao.Open();
                return await _conexao.GetAsync<TDbModel>(id);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public async Task<IEnumerable<TDbModel>> BuscarTodos()
        {
            try
            {
                _conexao.Open();
                return await _conexao.GetAllAsync<TDbModel>();
            }
            finally
            {
                _conexao.Close();
            }
        }

        public async Task Salvar(TDbModel dbModel)
        {
            try
            {
                _conexao.Open();
                await _conexao.InsertAsync(dbModel);
            }
            finally
            {
                _conexao.Close();
            }
        }

        public async Task<bool> Atualizar(TDbModel dbModel)
        {
            try
            {
                _conexao.Open();
                return await _conexao.UpdateAsync(dbModel);
            }
            finally
            {
                _conexao.Close();
            }
        }
    }
}
