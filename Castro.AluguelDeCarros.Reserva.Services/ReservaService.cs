using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Domain.Models;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IVeiculoService _veiculoService;

        public ReservaService(IReservaRepository reservaRepository, IVeiculoService veiculoService)
        {
            _reservaRepository = reservaRepository;
            _veiculoService = veiculoService;
        }

        public async Task<Cotacao> Cotar(CotarModel model)
        {
            //var veiculo = _veiculoService

            //var cotacao = new Cotacao(model.VeiculoId, model.TotalHoras, model.ClienteId);

            //_reservaRepository.SalvarCotacaoNoCache(cotacao);

            //return cotacao;

            return null;
        }

        public async Task<Domain.Reserva> ConfirmarCotacao(ConfirmarCotacaoModel model)
        {
            var cotacao = _reservaRepository.BuscarCotacaoNoCache(model.CotacaoId);

            if (cotacao != null)
            {
                var reserva = new Domain.Reserva(cotacao);
                if (reserva.Valido)
                    await _reservaRepository.SalvarReserva(reserva);

                return reserva;
            }
            else
                return null;
        }
    }
}
