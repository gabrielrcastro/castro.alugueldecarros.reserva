using Castro.AluguelDeCarros.Reserva.API.Filters;
using Castro.AluguelDeCarros.Reserva.API.Results;
using Castro.AluguelDeCarros.Reserva.Domain.Models;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
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
            try
            {
                var resultado = await _reservaService.Cotar(model);

                if (resultado == null)
                    return NotFound();
                if (!resultado.Valido)
                    return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao realizar cotação.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [Authorize("Bearer")]
        [ClaimRequirement(ClaimTypes.Role, RolesController.RoleCliente)]
        [HttpPost]
        [Route("confirmacao")]
        public async Task<IActionResult> ConfirmarCotacao(Guid cotacaoId)
        {
            try
            {
                var resultado = await _reservaService.ConfirmarCotacao(cotacaoId);

                if (resultado == null)
                    return NotFound();
                if (!resultado.Valido)
                    return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao confirmar reserva pela cotação.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [Authorize("Bearer")]
        [ClaimRequirement(ClaimTypes.Role, RolesController.RoleCliente)]
        [HttpGet]
        public async Task<IActionResult> ObterReservasDoCliente()
        {
            try
            {
                var resultado = await _reservaService.BuscarReservasDoClienteLogado();

                if (resultado == null)
                    return NotFound();
                if (resultado.Any(c => !c.Valido))
                    return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter reservas do cliente.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

    }
}
