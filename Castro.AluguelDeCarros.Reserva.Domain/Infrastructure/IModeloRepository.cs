using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Infrastructure
{
    public interface IModeloRepository
    {
        Task<Modelo> BuscarModelo(Guid id);

        Task<IEnumerable<Modelo>> BuscarTodosModelos();

        Task SalvarModelo(Modelo marca);

        Task AtualizarModelo(Modelo marca);
    }
}
