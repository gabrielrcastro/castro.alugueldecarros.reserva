using System;

namespace Castro.AluguelDeCarros.Reserva.Domain.Models
{
    public class CadastrarClienteModel
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public CadastrarClienteEnderecoModel Endereco { get; set; }
    }

    public class CadastrarClienteEnderecoModel
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
