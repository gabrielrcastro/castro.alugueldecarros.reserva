using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Infrastructure
{
    public interface IReservaRepository
    {
        Task SalvarReserva(Reserva reserva);
        Cotacao BuscarCotacaoNoCache(Guid id);
        void SalvarCotacaoNoCache(Cotacao cotacao);
        Task<IEnumerable<Reserva>> BuscarReservasPorCliente(Guid clienteId);
    }
}
