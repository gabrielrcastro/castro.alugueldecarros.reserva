using System;

namespace Castro.AluguelDeCarros.Reserva.Domain.Models
{
    public class CotarModel
    {
        public Guid VeiculoId { get; set; }
        public int QuantidadeHoras { get; set; }
    }
}
