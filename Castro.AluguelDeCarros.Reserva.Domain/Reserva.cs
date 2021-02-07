using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Reserva : DomainBase
    {
        public Reserva(Cotacao cotacao)
        {
            Id = Guid.NewGuid();
            VeiculoId = cotacao.VeiculoId;
            TotalHoras = cotacao.TotalHoras;
            ClienteId = cotacao.ClienteId;
            DataCriacao = DateTime.Now;

            var resultadoValidacao = new ReservaValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public Guid VeiculoId { get; private set; }
        public int TotalHoras { get; private set; }
        public decimal ValorTotal { get; private set; }
        public Guid ClienteId { get; private set; }
    }


    public class ReservaValidator : AbstractValidator<Reserva>
    {
        public ReservaValidator()
        {
            RuleFor(reserva => reserva.Id).NotNull();
            RuleFor(reserva => reserva.VeiculoId).NotNull().WithMessage("O veículo não foi informado");
            RuleFor(reserva => reserva.TotalHoras).NotNull().WithMessage("O total de horas não foi informado");
            RuleFor(reserva => reserva.ClienteId).NotNull().WithMessage("O cliente não foi informado");
        }
    }
}
