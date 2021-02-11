using Castro.AluguelDeCarros.Reserva.Domain.Models;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioBase> Salvar(CadastrarClienteModel model);
        Task<Autenticacao> Autenticar(string login, string senha);
    }
}
