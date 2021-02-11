using Castro.AluguelDeCarros.Reserva.API.Results;
using Castro.AluguelDeCarros.Reserva.Domain.Models;
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
            var resultado = await _modeloService.BuscarTodos();

            if (resultado == null)
                return NotFound();
            if (resultado.Any(c => !c.Valido))
                return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

            return Ok(resultado);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterModelo(Guid id)
        {
            var resultado = await _modeloService.Obter(id);

            if (resultado == null)
                return NotFound();
            if (!resultado.Valido)
                return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

            return Ok(resultado);
        }

        [HttpPost]
        [Route("{nome}")]
        public async Task<IActionResult> Cadastrar(ModeloModel model)
        {
            var resultado = await _modeloService.Salvar(null, model);

            if (resultado == null)
                return NotFound();
            if (!resultado.Valido)
                return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

            return Ok(resultado);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, ModeloModel model)
        {
            var resultado = await _modeloService.Salvar(id, model);

            if (resultado == null)
                return NotFound();
            if (!resultado.Valido)
                return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

            return Ok(resultado);
        }

        [HttpGet]
        [Route("{id}/veiculos")]
        public async Task<IActionResult> ObterVeiculosPorModelo(Guid id)
        {
            var resultado = await _veiculoService.BuscarVeiculosPorModelo(id);

            if (resultado == null)
                return NotFound();
            if (resultado.Any(c => !c.Valido))
                return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

            return Ok(resultado);
        }
    }
}
