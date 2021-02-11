using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Domain.Models;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Services
{
    public class ReservaService : ServiceBase, IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IVeiculoService _veiculoService;

        public ReservaService(IReservaRepository reservaRepository, IVeiculoService veiculoService, IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _reservaRepository = reservaRepository;
            _veiculoService = veiculoService;
        }

        public async Task<IEnumerable<Domain.Reserva>> BuscarReservasDoClienteLogado()
        {
            var clienteId = ObterIdUsuarioLogado();
            return await _reservaRepository.BuscarReservasPorCliente(clienteId);
        }

        public async Task<Cotacao> Cotar(CotarModel model)
        {
            Cotacao cotacao;

            var veiculo = await _veiculoService.Obter(model.VeiculoId);

            cotacao = new Cotacao(null, veiculo, model.QuantidadeHoras, null);

            if (cotacao.Valido)
                _reservaRepository.SalvarCotacaoNoCache(cotacao);

            return cotacao;
        }

        public async Task<Domain.Reserva> ConfirmarCotacao(Guid cotacaoId)
        {
            var cotacao = _reservaRepository.BuscarCotacaoNoCache(cotacaoId);

            if (cotacao != null && cotacao.Valido)
            {
                //ToDo: Substituir pelo usuário logado
                var obterUsuarioLogado = ObterIdUsuarioLogado();

                var reserva = new Domain.Reserva(cotacao, obterUsuarioLogado);
                if (reserva.Valido)
                    await _reservaRepository.SalvarReserva(reserva);

                return reserva;
            }
            else
                return null;
        }
    }
}
