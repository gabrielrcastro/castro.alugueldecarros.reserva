using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure
{
    public class VeiculoRepository : BaseRepository<VeiculoDbModel>, IVeiculoRepository
    {
        private readonly IDbConnection _conexao;

        public VeiculoRepository(IDbConnection conexao) : base(conexao)
        {
            _conexao = conexao;
        }

        public async Task<Veiculo> BuscarVeiculo(Guid id)
        {
            return ConverteDbModelParaDomain(await base.Buscar(id));
        }

        public async Task<IEnumerable<Veiculo>> BuscarTodosVeiculosPorCategoria(CategoriaEnum categoria)
        {
            var retorno = new List<Veiculo>();

            var query = @"SELECT * FROM Veiculo WHERE categoria = @categoria";

            DynamicParameters param = new DynamicParameters();
            param.Add("categoria", (int)categoria);

            var veiculosDb = await _conexao.QueryAsync<VeiculoDbModel>(query, param);

            foreach (var veiculo in veiculosDb)
                retorno.Add(ConverteDbModelParaDomain(veiculo));

            return retorno;
        }

        public async Task SalvarVeiculo(Veiculo veiculo)
        {
            await base.Salvar(ConverteDomainParaDbModel(veiculo));
        }

        public async Task AtualizarVeiculo(Veiculo veiculo)
        {
            await base.Salvar(ConverteDomainParaDbModel(veiculo));
        }

        #region Privado
        private VeiculoDbModel ConverteDomainParaDbModel(Veiculo veiculo)
        {
            return new VeiculoDbModel
            {
                Id = veiculo.Id,
                Placa = veiculo.Placa,
                ModeloId = veiculo.ModeloId,
                Ano = veiculo.Ano,
                ValorHora = veiculo.ValorHora,
                Combustivel = (int)veiculo.Combustivel,
                LimitePortaMalas = veiculo.LimitePortaMalas,
                Categoria = (int)veiculo.Categoria,
                DataCriacao = veiculo.DataCriacao,
                DataAlteracao = veiculo.DataAlteracao
            };
        }

        private Veiculo ConverteDbModelParaDomain(VeiculoDbModel veiculoDbModel)
        {
            return new Veiculo(veiculoDbModel.Id, veiculoDbModel.Placa, veiculoDbModel.ModeloId, veiculoDbModel.Ano, veiculoDbModel.ValorHora, (CombustivelEnum)veiculoDbModel.Combustivel,
                veiculoDbModel.LimitePortaMalas, (CategoriaEnum)veiculoDbModel.Categoria, veiculoDbModel.DataCriacao, veiculoDbModel.DataAlteracao);
        }
        #endregion
    }
}
