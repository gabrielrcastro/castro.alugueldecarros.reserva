using Castro.AluguelDeCarros.Reserva.Domain.Models;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Services
{
    public interface IReservaService
    {
        Task<Cotacao> Cotar(CotarModel model);

        Task<Reserva> ConfirmarCotacao(ConfirmarCotacaoModel model);
    }
}
