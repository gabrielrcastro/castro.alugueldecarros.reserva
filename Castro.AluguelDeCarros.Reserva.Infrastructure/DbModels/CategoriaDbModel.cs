using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    [Table("Categoria")]
    public class CategoriaDbModel : BaseDbModel
    {
        public string Nome { get; set; }

        [Write(false)]
        [Computed]
        public virtual List<VeiculoDbModel> Veiculos { get; set; }
    }
}
