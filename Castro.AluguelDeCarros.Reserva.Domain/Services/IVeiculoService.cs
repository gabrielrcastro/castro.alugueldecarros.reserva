using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Services
{
    public interface IVeiculoService
    {
        Task<IEnumerable<Categoria>> BuscarVeiculosPorCategoria();
    }
}
