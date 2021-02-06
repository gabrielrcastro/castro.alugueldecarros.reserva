using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Infrastructure
{
    public interface IMarcaRepository
    {
        Task<Marca> BuscarMarca(Guid id);

        Task<IEnumerable<Marca>> BuscarTodasMarcas();

        Task SalvarMarca(Marca marca);

        Task AtualizarMarca(Marca marca);
    }
}
