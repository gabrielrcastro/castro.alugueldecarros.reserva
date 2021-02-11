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
    public class ModeloController : ControllerBase
    {
        private readonly ILogger<ModeloController> _logger;
        private readonly IVeiculoService _veiculoService;
        private readonly IModeloService _modeloService;

        public ModeloController(ILogger<ModeloController> logger, IVeiculoService veiculoService, IModeloService modeloService)
        {
            _logger = logger;
            _veiculoService = veiculoService;
            _modeloService = modeloService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterModelos()
        {
            try
            {
                var resultado = await _modeloService.BuscarTodos();

                if (resultado == null)
                    return NotFound();
                if (resultado.Any(c => !c.Valido))
                    return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter modelos.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterModelo(Guid id)
        {
            try
            {
                var resultado = await _modeloService.Obter(id);

                if (resultado == null)
                    return NotFound();
                if (!resultado.Valido)
                    return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter modelo.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [Authorize("Bearer")]
        [ClaimRequirement(ClaimTypes.Role, RolesController.RoleOperador)]
        [HttpPost]
        [Route("{nome}")]
        public async Task<IActionResult> Cadastrar(ModeloModel model)
        {
            try
            {
                var resultado = await _modeloService.Salvar(null, model);

                if (resultado == null)
                    return NotFound();
                if (!resultado.Valido)
                    return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao cadastrar modelo.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [Authorize("Bearer")]
        [ClaimRequirement(ClaimTypes.Role, RolesController.RoleOperador)]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, ModeloModel model)
        {
            try
            {
                var resultado = await _modeloService.Salvar(id, model);

                if (resultado == null)
                    return NotFound();
                if (!resultado.Valido)
                    return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao atualizar modelo.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}/veiculos")]
        public async Task<IActionResult> ObterVeiculosPorModelo(Guid id)
        {
            try
            {
                var resultado = await _veiculoService.BuscarVeiculosPorModelo(id);

                if (resultado == null)
                    return NotFound();
                if (resultado.Any(c => !c.Valido))
                    return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao obter veículos pelo modelo.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }
    }
}
