using Castro.AluguelDeCarros.Reserva.Domain.Models;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly ILogger<ReservaController> _logger;
        private readonly IReservaService _reservaService;

        public ReservaController(ILogger<ReservaController> logger, IReservaService reservaService)
        {
            _logger = logger;
            _reservaService = reservaService;
        }

        [HttpPost]
        [Route("cotacao")]
        public async Task<IActionResult> Cotar(CotarModel model)
        {
            return Ok(await _reservaService.Cotar(model));
        }

        [HttpPost]
        [Route("confirmacao")]
        public async Task<IActionResult> ConfirmarCotacao(ConfirmarCotacaoModel model)
        {
            return Ok(await _reservaService.ConfirmarCotacao(model));
        }
    }
}
