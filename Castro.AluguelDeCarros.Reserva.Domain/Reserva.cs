using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Reserva : DomainBase
    {
        public Reserva(Cotacao cotacao, Guid clienteId) : base(null, null)
        {
            VeiculoId = cotacao.VeiculoId;
            Horas = cotacao.TotalHoras;
            Valor = cotacao.ValorTotal;
            ClienteId = clienteId;

            var resultadoValidacao = new ReservaValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public Reserva(Guid id, Guid veiculoId, int totalHoras, decimal valorTotal, Guid clienteId, DateTime dataCriacao) : base(id, dataCriacao)
        {
            VeiculoId = veiculoId;
            Horas = totalHoras;
            Valor = valorTotal;
            ClienteId = clienteId;

            var resultadoValidacao = new ReservaValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public Guid VeiculoId { get; private set; }
        public int Horas { get; private set; }
        public decimal Valor { get; private set; }
        public Guid ClienteId { get; private set; }
    }

    public class ReservaValidator : AbstractValidator<Reserva>
    {
        public ReservaValidator()
        {
            RuleFor(reserva => reserva.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(reserva => reserva.VeiculoId).NotNull().NotEqual(Guid.Empty).WithMessage("O veículo não foi informado");
            RuleFor(reserva => reserva.Horas).NotNull().GreaterThan(0).WithMessage("O total de horas não foi informado");
            RuleFor(reserva => reserva.Valor).NotNull().GreaterThan(0).WithMessage("O total de horas não foi informado");
            RuleFor(reserva => reserva.ClienteId).NotNull().NotEqual(Guid.Empty).WithMessage("O cliente não foi informado");
        }
    }
}
