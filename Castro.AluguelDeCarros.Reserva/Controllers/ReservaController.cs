using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Castro.AluguelDeCarros.Reserva.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly ILogger<ReservaController> _logger;

        public ReservaController(ILogger<ReservaController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IEnumerable<string> Post()
        {
            return null;
        }
    }
}
