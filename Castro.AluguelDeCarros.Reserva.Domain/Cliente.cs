using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Cliente : DomainBase
    {
        public Cliente(Guid? id, string nome, string cpf, DateTime dataDeNascimento, string enderecoCep, string enderecoLogradouro,
            string enderecoNumero, string enderecoComplemento, string enderecoCidade, string enderecoEstado, DateTime dataCriacao) : base(id, dataCriacao)
        {
            Nome = nome;
            Cpf = cpf;
            DataDeNascimento = dataDeNascimento;
            Endereco = new Endereco(enderecoCep, enderecoLogradouro, enderecoNumero, enderecoComplemento, enderecoCidade, enderecoEstado);

            var resultadoValidacao = new ClienteValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            if (!Endereco.Valido)
                Erros.AddRange(Endereco.Erros);

            Valido = resultadoValidacao.IsValid;
        }

        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataDeNascimento { get; private set; }
        public Endereco Endereco { get; private set; }
    }

    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(reserva => reserva.Id).NotNull();
            RuleFor(reserva => reserva.Nome).NotNull().WithMessage("O nome do cliente não foi informado.");
            RuleFor(reserva => reserva.Cpf).NotNull().WithMessage("O CPF do cliente não foi informado.");
            RuleFor(reserva => reserva.DataDeNascimento).GreaterThan(DateTime.MinValue).WithMessage("A Data de Nascimento não foi informada.");
        }
    }
}
