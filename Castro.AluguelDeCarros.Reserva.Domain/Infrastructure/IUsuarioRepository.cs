using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Infrastructure
{
    public interface IUsuarioRepository
    {
        Task<UsuarioBase> BuscarUsuarioPorLoginESenha(string login, string senha);

        Task SalvarUsuario(UsuarioBase usuario);
    }
}
