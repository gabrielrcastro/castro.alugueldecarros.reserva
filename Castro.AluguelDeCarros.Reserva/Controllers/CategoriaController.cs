using Castro.AluguelDeCarros.Reserva.API.Results;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ICategoriaService _categoriaService;
        private readonly IVeiculoService _veiculoService;

        public CategoriaController(ILogger<CategoriaController> logger, ICategoriaService categoriaService, IVeiculoService veiculoService)
        {
            _logger = logger;
            _categoriaService = categoriaService;
            _veiculoService = veiculoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterCategorias()
        {
            var resultado = await _categoriaService.BuscarTodas();

            if (resultado == null)
                return NotFound();
            if (resultado.Any(c => !c.Valido))
                return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

            return Ok(resultado);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterCategoria(Guid id)
        {
            var resultado = await _categoriaService.Obter(id);

            if (resultado == null)
                return NotFound();
            if (!resultado.Valido)
                return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

            return Ok(resultado);
        }

        [HttpGet]
        [Route("veiculos")]
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
