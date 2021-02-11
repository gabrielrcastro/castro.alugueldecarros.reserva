using Castro.AluguelDeCarros.Reserva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Services
{
    public interface IReservaService
    {
        Task<IEnumerable<Reserva>> BuscarReservasDoClienteLogado();
        Task<Cotacao> Cotar(CotarModel model);
        Task<Reserva> ConfirmarCotacao(Guid cotacaoId);
    }
}
