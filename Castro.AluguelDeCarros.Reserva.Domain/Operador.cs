using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Operador : DomainBase
    {
        public Operador(Guid? id, string nome, string matricula)
        {
            if (!id.HasValue)
                Id = Guid.NewGuid();

            Nome = nome;
            Matricula = matricula;

            var resultadoValidacao = new OperadorValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);

            Valido = resultadoValidacao.IsValid;
        }

        public string Nome { get; private set; }
        public string Matricula { get; private set; }
    }

    public class OperadorValidator : AbstractValidator<Operador>
    {
        public OperadorValidator()
        {
            RuleFor(reserva => reserva.Id).NotNull();
            RuleFor(reserva => reserva.Nome).NotNull().WithMessage("O Nome do Operador não foi informado.");
            RuleFor(reserva => reserva.Matricula).NotNull().NotEmpty().WithMessage("A Matrícula do Operador não foi informada.");
        }
    }
}
