using Castro.AluguelDeCarros.Reserva.API.Results;
using Castro.AluguelDeCarros.Reserva.Domain.Models;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CadastrarClienteModel model)
        {
            try
            {
                var resultado = await _usuarioService.Salvar(model);

                if (resultado == null)
                    return NotFound();
                if (!resultado.Valido)
                    return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao cadastrar usuário (cliente).", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("autenticacao")]
        public async Task<IActionResult> Autenticar(AutenticacaoModel model)
        {
            try
            {
                var resultado = await _usuarioService.Autenticar(model.Login, model.Senha);

                if (resultado == null)
                    return NotFound();
                if (!resultado.Valido)
                    return new BadRequestObjectResult(new ErrorResult(resultado.Erros).ToResult());

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao autenticar usuário.", string.Concat(ex.Message, ex.StackTrace));
                return StatusCode(500);
            }
        }
    }
}
