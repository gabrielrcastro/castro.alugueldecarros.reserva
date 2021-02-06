using Dapper.Contrib.Extensions;
using System;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    [Table("Modelo")]
    public class ModeloDbModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Guid MarcaId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
