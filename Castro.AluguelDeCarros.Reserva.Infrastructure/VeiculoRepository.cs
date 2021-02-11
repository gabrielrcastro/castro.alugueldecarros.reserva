using AutoMapper;
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
        private readonly IMapper _mapper;

        public VeiculoRepository(IDbConnection conexao, IMapper mapper) : base(conexao)
        {
            _conexao = conexao;
            _mapper = mapper;
        }

        public async Task<Veiculo> BuscarVeiculo(Guid id)
        {
            return _mapper.Map<Veiculo>(await base.Buscar(id));
        }

        public async Task SalvarVeiculo(Veiculo veiculo)
        {
            await base.Salvar(_mapper.Map<VeiculoDbModel>(veiculo));
        }

        public async Task AtualizarVeiculo(Veiculo veiculo)
        {
            await base.Atualizar(_mapper.Map<VeiculoDbModel>(veiculo));
        }

        public async Task<IEnumerable<Veiculo>> BuscarTodosVeiculos()
        {
            return _mapper.Map<List<Veiculo>>(await base.BuscarTodos());
        }

        public async Task<IEnumerable<Categoria>> BuscarTodosVeiculosPorCategoria()
        {
            var retorno = new List<Categoria>();

            var query = @"SELECT c.id, c.nome, v.id, v.placa, v.modeloId, v.ano, v.valorHora, v.combustivel, v.limitePortaMalas, 
                            v.categoriaId, v.dataCriacao, v.DataAlteracao
                        FROM Categoria c 
                        INNER JOIN Veiculo v 
                            ON v.categoriaId = c.id";

            var relacao = new Dictionary<Guid, CategoriaDbModel>();
            var categoriasDb = await _conexao.QueryAsync<CategoriaDbModel, VeiculoDbModel, CategoriaDbModel>(query,
                                    (categoria, veiculo) =>
                                    {
                                        if (!relacao.TryGetValue(categoria.Id, out CategoriaDbModel encontrado))
                                            relacao.Add(categoria.Id, encontrado = categoria);

                                        if (encontrado.Veiculos == null)
                                            encontrado.Veiculos = new List<VeiculoDbModel>();

                                        encontrado.Veiculos.Add(veiculo);
                                        return encontrado;
                                    });

            return _mapper.Map<List<Categoria>>(relacao?.Values);
        }

        public async Task<IEnumerable<Marca>> BuscarVeiculosPorMarca(Guid id)
        {
            var retorno = new List<Marca>();

            var parameters = new DynamicParameters();
            parameters.Add("marcaId", id);

            var query = @"SELECT ma.id, ma.nome, v.id, v.placa, v.modeloId, v.ano, v.valorHora, v.combustivel, v.limitePortaMalas, 
                            v.categoriaId, v.dataCriacao, v.DataAlteracao
                        FROM Veiculo v 
                        INNER JOIN Modelo mo
                            ON mo.id = v.modeloId
                        INNER JOIN Marca ma 
                            ON ma.id = mo.marcaId                        
                        WHERE ma.id = @marcaId";

            var relacao = new Dictionary<Guid, MarcaDbModel>();
            var categoriasDb = await _conexao.QueryAsync<MarcaDbModel, VeiculoDbModel, MarcaDbModel>(query,
                                    (marca, veiculo) =>
                                    {
                                        if (!relacao.TryGetValue(marca.Id, out MarcaDbModel encontrado))
                                            relacao.Add(marca.Id, encontrado = marca);

                                        if (encontrado.Veiculos == null)
                                            encontrado.Veiculos = new List<VeiculoDbModel>();

                                        encontrado.Veiculos.Add(veiculo);
                                        return encontrado;
                                    }, parameters);

            return _mapper.Map<List<Marca>>(relacao?.Values);
        }

        public async Task<IEnumerable<Modelo>> BuscarVeiculosPorModelo(Guid id)
        {
            var retorno = new List<Modelo>();

            var parameters = new DynamicParameters();
            parameters.Add("modeloId", id);

            var query = @"SELECT mo.id, mo.nome, v.id, v.placa, v.modeloId, v.ano, v.valorHora, v.combustivel, v.limitePortaMalas, 
                            v.categoriaId, v.dataCriacao, v.DataAlteracao
                        FROM Veiculo v 
                        INNER JOIN Modelo mo
                            ON mo.id = v.modeloId                 
                        WHERE mo.id = @modeloId";

            var relacao = new Dictionary<Guid, ModeloDbModel>();
            var categoriasDb = await _conexao.QueryAsync<ModeloDbModel, VeiculoDbModel, ModeloDbModel>(query,
                                    (modelo, veiculo) =>
                                    {
                                        if (!relacao.TryGetValue(modelo.Id, out ModeloDbModel encontrado))
                                            relacao.Add(modelo.Id, encontrado = modelo);

                                        if (encontrado.Veiculos == null)
                                            encontrado.Veiculos = new List<VeiculoDbModel>();

                                        encontrado.Veiculos.Add(veiculo);
                                        return encontrado;
                                    }, parameters);

            return _mapper.Map<List<Modelo>>(relacao?.Values);
        }
    }
}
