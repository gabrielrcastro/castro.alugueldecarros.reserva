using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Castro.AluguelDeCarros.Reserva.Services
{
    public abstract class ServiceBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ServiceBase(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected Guid ObterIdUsuarioLogado()
        {
            if (_contextAccessor?.HttpContext?.User?.Identity != null && _contextAccessor.HttpContext.User.Identity is ClaimsIdentity identity && identity.Claims != null)
            {
                var usuarioId = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(usuarioId))
                    return new Guid(usuarioId);
            }
            return Guid.Empty;
        }
    }
}
