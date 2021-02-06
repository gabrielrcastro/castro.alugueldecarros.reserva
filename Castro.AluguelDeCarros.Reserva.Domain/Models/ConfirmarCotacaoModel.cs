using System;

namespace Castro.AluguelDeCarros.Reserva.Domain.Models
{
    public class ConfirmarCotacaoModel
    {
        public Guid CotacaoId { get; set; }
        public Guid ClienteId { get; set; }
    }
}
