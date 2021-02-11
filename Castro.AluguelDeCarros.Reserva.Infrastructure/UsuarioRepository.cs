using AutoMapper;
using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using Castro.AluguelDeCarros.Reserva.Domain.Infrastructure;
using Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels;
using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure
{
    public class UsuarioRepository : BaseRepository<UsuarioDbModel>, IUsuarioRepository
    {
        private readonly IDbConnection _conexao;
        private readonly IMapper _mapper;

        public UsuarioRepository(IDbConnection conexao, IMapper mapper) : base(conexao)
        {
            _conexao = conexao;
            _mapper = mapper;
        }

        public async Task<UsuarioBase> BuscarUsuarioPorLoginESenha(string login, string senha)
        {
            var query = @"SELECT id, login, nome, tipo, cpf, matricula, dataNascimento, enderecoCep, 
                            enderecoLogradouro, enderecoNumero, enderecoComplemento, enderecoCidade, 
                            enderecoEstado, dataCriacao, dataAlteracao
                        FROM Usuario 
                        WHERE login = @login AND senha = @senha";

            var parameters = new DynamicParameters();
            parameters.Add("login", login);
            parameters.Add("senha", senha);

            var usuarioDb = await _conexao.QueryFirstOrDefaultAsync<UsuarioDbModel>(query, parameters);

            if (usuarioDb != null && (TipoUsuarioEnum)usuarioDb.Tipo == TipoUsuarioEnum.Operador)
                return _mapper.Map<Operador>(usuarioDb);
            else
                return _mapper.Map<Cliente>(usuarioDb);
        }

        public async Task SalvarUsuario(UsuarioBase usuario)
        {
            await base.Salvar(_mapper.Map<UsuarioDbModel>(usuario));
        }
    }
}
