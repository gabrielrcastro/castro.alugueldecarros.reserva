using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly IVeiculoService _veiculoService;

        public CategoriaController(ILogger<CategoriaController> logger, IVeiculoService veiculoService)
        {
            _logger = logger;
            _veiculoService = veiculoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterCategorias()
        {
            return null;
        }

        [HttpGet]
        [Route("{codigoCategoria}/veiculos")]
        public async Task<IActionResult> ObterVeiculosPorCategoria(CategoriaEnum codigoCategoria)
        {
            var resultado = await _veiculoService.BuscarVeiculosPorCategoria(codigoCategoria);

            if (resultado == null)
                return NotFound();
            if (resultado.Any(c => !c.Valido))
                return BadRequest(resultado.Select(c => c.Erros));

            return Ok(resultado);
        }
    }
}
