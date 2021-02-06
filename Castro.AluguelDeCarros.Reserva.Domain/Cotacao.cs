using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Cotacao : DomainBase
    {
        public Cotacao(Guid? id, Guid veiculoId, int totalHoras, Guid clienteId, decimal valorTotal)
        {
            if (!id.HasValue)
                Id = Guid.NewGuid();

            VeiculoId = veiculoId;
            TotalHoras = totalHoras;
            ClienteId = clienteId;
            ValorTotal = valorTotal;

            var resultadoValidacao = new CotacaoValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros = resultadoValidacao.Errors;
            Valido = resultadoValidacao.IsValid;
        }

        public Guid Id { get; private set; }
        public Guid VeiculoId { get; private set; }
        public int TotalHoras { get; private set; }
        public decimal ValorTotal { get; private set; }
        public Guid ClienteId { get; private set; }
    }

    public class CotacaoValidator : AbstractValidator<Cotacao>
    {
        public CotacaoValidator()
        {
            RuleFor(cotacao => cotacao.Id).NotNull();
            RuleFor(cotacao => cotacao.VeiculoId).NotNull().WithMessage("O veículo não foi informado.");
            RuleFor(cotacao => cotacao.TotalHoras).GreaterThan(0).WithMessage("O total de horas não foi informado.");
            RuleFor(cotacao => cotacao.ClienteId).NotNull().WithMessage("O cliente não foi informado.");
            RuleFor(cotacao => cotacao.ValorTotal).GreaterThan(0).WithMessage("Não foi possível carregar o Valor total corretamente.");
        }
    }
}
