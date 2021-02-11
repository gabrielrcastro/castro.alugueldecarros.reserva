using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Operador : UsuarioBase
    {
        public Operador(Guid? id, string matricula, string senha, string nome, DateTime? dataCriacao) : base(id, matricula, senha, nome, dataCriacao)
        {
            Matricula = matricula;
            Tipo = Enums.TipoUsuarioEnum.Operador;

            var resultadoValidacao = new OperadorValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);

            Valido = resultadoValidacao.IsValid;
        }
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
