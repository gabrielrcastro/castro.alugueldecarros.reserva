using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Cotacao : DomainBase
    {
        public Cotacao(Guid? id, Guid veiculoId, int totalHoras, Guid? clienteId, decimal valorTotal, DateTime? dataCriacao) : base(id, dataCriacao)
        {
            VeiculoId = veiculoId;
            TotalHoras = totalHoras;
            ClienteId = clienteId;
            ValorTotal = valorTotal;

            var resultadoValidacao = new CotacaoValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public Guid VeiculoId { get; private set; }
        public int TotalHoras { get; private set; }
        public decimal ValorTotal { get; private set; }
        public Guid? ClienteId { get; private set; }
    }

    public class CotacaoValidator : AbstractValidator<Cotacao>
    {
        public CotacaoValidator()
        {
            RuleFor(cotacao => cotacao.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(cotacao => cotacao.VeiculoId).NotNull().NotEqual(Guid.Empty).WithMessage("O veículo não foi informado.");
            RuleFor(cotacao => cotacao.TotalHoras).GreaterThan(0).WithMessage("O total de horas não foi informado.");
            RuleFor(cotacao => cotacao.ClienteId).NotNull().NotEqual(Guid.Empty).WithMessage("O cliente não foi informado.");
            RuleFor(cotacao => cotacao.ValorTotal).GreaterThan(0).WithMessage("Não foi possível carregar o Valor total corretamente.");
        }
    }
}
