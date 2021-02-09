using Dapper.Contrib.Extensions;
using System;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    [Table("Veiculo")]
    public class VeiculoDbModel : BaseDbModel
    {
        public string Placa { get; set; }
        public Guid ModeloId { get; set; }
        public Guid CategoriaId { get; set; }
        public DateTime Ano { get; set; }
        public decimal ValorHora { get; set; }
        public int Combustivel { get; set; }
        public float LimitePortaMalas { get; set; }
    }
}
