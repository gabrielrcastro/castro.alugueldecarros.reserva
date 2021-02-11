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
            try
            {
                var resultado = await _categoriaService.BuscarTodas();

                if (resultado == null)
                    return NotFound();
                if (resultado.Any(c => !c.Valido))
                    return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter categorias.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterCategoria(Guid id)
        {
            try
            {
                var resultado = await _categoriaService.Obter(id);

                if (resultado == null)
                    return NotFound();
                if (!resultado.Valido)
                    return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter categoria.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("veiculos")]
        public async Task<IActionResult> ObterVeiculosPorCategoria()
        {
            try
            {
                var resultado = await _veiculoService.BuscarVeiculosPorCategoria();

                if (resultado == null)
                    return NotFound();
                if (resultado.Any(c => !c.Valido))
                    return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter veículos por categoria.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }
    }
}
