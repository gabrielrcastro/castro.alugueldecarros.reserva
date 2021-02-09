using Dapper.Contrib.Extensions;
using System;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    [Table("Marca")]
    public class MarcaDbModel : BaseDbModel
    {
        public string Nome { get; set; }
    }
}
