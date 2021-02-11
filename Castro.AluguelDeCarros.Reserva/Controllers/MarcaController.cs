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
    public class MarcaController : ControllerBase
    {
        private readonly ILogger<MarcaController> _logger;
        private readonly IVeiculoService _veiculoService;
        private readonly IMarcaService _marcaService;

        public MarcaController(ILogger<MarcaController> logger, IVeiculoService veiculoService, IMarcaService marcaService)
        {
            _logger = logger;
            _veiculoService = veiculoService;
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterMarcas()
        {
            var resultado = await _marcaService.BuscarTodas();

            if (resultado == null)
                return NotFound();
            if (resultado.Any(c => !c.Valido))
                return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

            return Ok(resultado);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObterMarca(Guid id)
        {
            var resultado = await _marcaService.Obter(id);

            if (resultado == null)
                return NotFound();
            if (!resultado.Valido)
                return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

            return Ok(resultado);
        }

        [HttpPost]
        [Route("{nome}")]
        public async Task<IActionResult> Cadastrar(string nome)
        {
            var resultado = await _marcaService.Salvar(null, nome);

            if (resultado == null)
                return NotFound();
            if (!resultado.Valido)
                return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

            return Ok(resultado);
        }


        [HttpPut]
        [Route("{id}/{nomeNovo}")]
        public async Task<IActionResult> Atualizar(Guid id, string nomeNovo)
        {
            var resultado = await _marcaService.Salvar(id, nomeNovo);

            if (resultado == null)
                return NotFound();
            if (!resultado.Valido)
                return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

            return Ok(resultado);
        }

        [HttpGet]
        [Route("{id}/veiculos")]
        public async Task<IActionResult> ObterVeiculosPorMarca(Guid id)
        {
            var resultado = await _veiculoService.BuscarVeiculosPorMarca(id);

            if (resultado == null)
                return NotFound();
            if (resultado.Any(c => !c.Valido))
                return new BadRequestObjectResult(new ErrorResult(resultado.Select(c => c.Erros)).ToResult());

            return Ok(resultado);
        }
    }
}
