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
        public ModeloRepository(IDbConnection conexao) : base(conexao)
        { }

        public async Task<Modelo> BuscarModelo(Guid id)
        {
            return ConverteDbModelParaDomain(await base.Buscar(id));
        }

        public async Task<IEnumerable<Modelo>> BuscarTodosModelos()
        {
            var retornoModelos = new List<Modelo>();

            var modelosDb = await base.BuscarTodos();
            foreach (var modelo in modelosDb)
            {
                retornoModelos.Add(ConverteDbModelParaDomain(modelo));
            }

            return retornoModelos;
        }

        public async Task SalvarModelo(Modelo modelo)
        {
            await base.Salvar(ConverteDomainParaDbModel(modelo));
        }

        public async Task AtualizarModelo(Modelo modelo)
        {
            await base.Salvar(ConverteDomainParaDbModel(modelo));
        }

        #region Privado
        private ModeloDbModel ConverteDomainParaDbModel(Modelo modelo)
        {
            return new ModeloDbModel
            {
                Id = modelo.Id,
                Nome = modelo.Nome,
                MarcaId = modelo.MarcaId,
                DataCriacao = modelo.DataCriacao,
                DataAlteracao = modelo.DataAlteracao
            };
        }

        private Modelo ConverteDbModelParaDomain(ModeloDbModel modeloDbModel)
        {
            return new Modelo(modeloDbModel.Id, modeloDbModel.Nome, modeloDbModel.MarcaId);
        }
        #endregion
    }
}
