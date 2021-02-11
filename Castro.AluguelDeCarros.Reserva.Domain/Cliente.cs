using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Cliente : UsuarioBase
    {
        public Cliente(Guid? id, string nome, string cpf, string senha, DateTime dataDeNascimento, string enderecoCep, string enderecoLogradouro,
            string enderecoNumero, string enderecoComplemento, string enderecoCidade, string enderecoEstado, DateTime? dataCriacao) : base(id, cpf, senha, nome, dataCriacao)
        {
            Cpf = cpf;
            DataNascimento = dataDeNascimento;
            Endereco = new Endereco(enderecoCep, enderecoLogradouro, enderecoNumero, enderecoComplemento, enderecoCidade, enderecoEstado);
            Tipo = Enums.TipoUsuarioEnum.Cliente;

            var resultadoValidacao = new ClienteValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            if (!Endereco.Valido)
                Erros.AddRange(Endereco.Erros);

            Valido = resultadoValidacao.IsValid;
        }
    }

    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(reserva => reserva.Id).NotNull();
            RuleFor(reserva => reserva.Nome).NotNull().WithMessage("O nome do cliente não foi informado.");
            RuleFor(reserva => reserva.Cpf).NotNull().WithMessage("O CPF do cliente não foi informado.");
            RuleFor(reserva => reserva.DataNascimento).GreaterThan(DateTime.MinValue).WithMessage("A Data de Nascimento não foi informada.");
        }
    }
}
