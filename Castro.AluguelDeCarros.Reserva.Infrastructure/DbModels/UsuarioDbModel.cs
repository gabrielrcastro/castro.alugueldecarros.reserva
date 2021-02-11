using Dapper.Contrib.Extensions;
using System;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    [Table("Usuario")]
    public class UsuarioDbModel : BaseDbModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public int Tipo { get; set; }
        public string Cpf { get; set; }
        public string Matricula { get; set; }
        public DateTime DataNascimento { get; set; }
        public string EnderecoCep { get; set; }
        public string EnderecoLogradouro { get; set; }
        public string EnderecoNumero { get; set; }
        public string EnderecoComplemento { get; set; }
        public string EnderecoCidade { get; set; }
        public string EnderecoEstado { get; set; }
    }
}
