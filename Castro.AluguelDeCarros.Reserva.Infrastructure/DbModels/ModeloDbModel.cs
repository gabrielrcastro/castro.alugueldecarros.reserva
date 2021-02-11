using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    [Table("Modelo")]
    public class ModeloDbModel : BaseDbModel
    {
        public string Nome { get; set; }
        public Guid MarcaId { get; set; }

        [Write(false)]
        [Computed]
        public virtual List<VeiculoDbModel> Veiculos { get; set; }
    }
}
