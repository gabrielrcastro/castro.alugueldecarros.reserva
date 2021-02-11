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
        public async Task<IActionResult> ObterVeiculos()
        {
            try
            {
                var resultado = await _veiculoService.BuscarTodos();

                if (resultado == null)
                    return NotFound();
                if (resultado.Any(c => !c.Valido))
                    return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter veículos.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterVeiculo(Guid id)
        {
            try
            {
                var resultado = await _veiculoService.Obter(id);

                if (resultado == null)
                    return NotFound();
                if (!resultado.Valido)
                    return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter veículo.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [Authorize("Bearer")]
        [ClaimRequirement(ClaimTypes.Role, RolesController.RoleOperador)]
        [HttpPost]
        public async Task<IActionResult> Cadastrar(VeiculoModel model)
        {
            try
            {
                var resultado = await _veiculoService.Salvar(null, model);

                if (resultado == null)
                    return NotFound();
                if (!resultado.Valido)
                    return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao cadatrar veículo.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [Authorize("Bearer")]
        [ClaimRequirement(ClaimTypes.Role, RolesController.RoleOperador)]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, VeiculoModel model)
        {
            try
            {
                var resultado = await _veiculoService.Salvar(id, model);

                if (resultado == null)
                    return NotFound();
                if (!resultado.Valido)
                    return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao atualizar veículo.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }
    }
}
