using Dapper.Contrib.Extensions;
using System;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    [Table("Reserva")]
    public class ReservaDbModel : BaseDbModel
    {
        public Guid VeiculoId { get; set; }
        public decimal Valor { get; set; }
        public int Horas { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
