using Dapper.Contrib.Extensions;
using System;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    [Table("Modelo")]
    public class ModeloDbModel : BaseDbModel
    {
        public string Nome { get; set; }
        public Guid MarcaId { get; set; }
    }
}
