using Dapper.Contrib.Extensions;
using System;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    [Table("Veiculo")]
    public class VeiculoDbModel
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public Guid ModeloId { get; set; }
        public DateTime Ano { get; set; }
        public decimal ValorHora { get; set; }
        public int Combustivel { get; set; }
        public float LimitePortaMalas { get; set; }
        public int Categoria { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
