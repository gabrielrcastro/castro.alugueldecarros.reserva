using System;

namespace Castro.AluguelDeCarros.Reserva.Domain.Models
{
    public class CotarModel
    {
        public Guid VeiculoId { get; set; }
        public int TotalHoras { get; set; }
        public Guid ClienteId { get; set; }
    }
}
