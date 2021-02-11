using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    [Table("Marca")]
    public class MarcaDbModel : BaseDbModel
    {
        public string Nome { get; set; }

        [Write(false)]
        [Computed]
        public virtual List<VeiculoDbModel> Veiculos { get; set; }
    }
}
