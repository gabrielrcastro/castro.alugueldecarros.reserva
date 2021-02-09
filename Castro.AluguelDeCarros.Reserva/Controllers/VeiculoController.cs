using Castro.AluguelDeCarros.Reserva.API.Results;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly ILogger<VeiculoController> _logger;
        private readonly IVeiculoService _veiculoService;

        public VeiculoController(ILogger<VeiculoController> logger, IVeiculoService veiculoService)
        {
            _logger = logger;
            _veiculoService = veiculoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterVeiculosPorCategoria()
        {
            var resultado = await _veiculoService.BuscarVeiculosPorCategoria();

            if (resultado == null)
                return NotFound();
            if (resultado.Any(c => !c.Valido))
                return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

            return Ok(resultado);
        }
    }
}
