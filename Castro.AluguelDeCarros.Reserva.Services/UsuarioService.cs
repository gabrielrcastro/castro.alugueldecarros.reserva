using AutoMapper;
using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Domain.Models;
using Castro.AluguelDeCarros.Reserva.Domain.Services;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioBase> Salvar(CadastrarClienteModel model)
        {
            Cliente usuario;

            usuario = _mapper.Map<Cliente>(model);
            await _usuarioRepository.SalvarUsuario(usuario);

            return usuario;
        }

        public async Task<Autenticacao> Autenticar(string login, string senha)
        {
            return new Autenticacao(await _usuarioRepository.BuscarUsuarioPorLoginESenha(login, senha));
        }
    }
}
