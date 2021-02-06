using Dapper.Contrib.Extensions;
using System;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    [Table("Reserva")]
    public class ReservaDbModel
    {
        public Guid Id { get; set; }
        public Guid VeiculoId { get; set; }
        public decimal Valor { get; set; }
        public int Horas { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
