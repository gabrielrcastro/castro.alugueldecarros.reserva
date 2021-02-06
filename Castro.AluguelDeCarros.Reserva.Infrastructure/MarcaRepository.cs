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
        public MarcaRepository(IDbConnection conexao) : base(conexao)
        { }

        public async Task<Marca> BuscarMarca(Guid id)
        {
            return ConverteDbModelParaDomain(await base.Buscar(id));
        }

        public async Task<IEnumerable<Marca>> BuscarTodasMarcas()
        {
            var retornoMarcas = new List<Marca>();

            var marcasDb = await base.BuscarTodos();
            foreach (var marca in marcasDb)
            {
                retornoMarcas.Add(ConverteDbModelParaDomain(marca));
            }

            return retornoMarcas;
        }

        public async Task SalvarMarca(Marca marca)
        {
            await base.Salvar(ConverteDomainParaDbModel(marca));
        }

        public async Task AtualizarMarca(Marca marca)
        {
            await base.Salvar(ConverteDomainParaDbModel(marca));
        }

        #region Privado
        private MarcaDbModel ConverteDomainParaDbModel(Marca marca)
        {
            return new MarcaDbModel
            {
                Id = marca.Id,
                Nome = marca.Nome,
                DataCriacao = marca.DataCriacao,
                DataAlteracao = marca.DataAlteracao
            };
        }

        private Marca ConverteDbModelParaDomain(MarcaDbModel marcaDbModel)
        {
            return new Marca(marcaDbModel.Id, marcaDbModel.Nome);
        }
        #endregion
    }
}
