using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Cotacao : DomainBase
    {
        public Cotacao(Guid? id, Veiculo veiculo, int totalHoras, DateTime? dataCriacao) : base(id, dataCriacao)
        {
            if (veiculo == null || veiculo.Id == Guid.Empty || !veiculo.Valido)
            {
                Valido = false;
                Erros.Add(new FluentValidation.Results.ValidationFailure("VeiculoId", "O veículo informado é inválido ou não existe."));
            }
            else if (totalHoras > 0)
                ValorTotal = veiculo.ValorHora * totalHoras;

            VeiculoId = veiculo.Id;
            TotalHoras = totalHoras;

            var resultadoValidacao = new CotacaoValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public Guid VeiculoId { get; private set; }
        public int TotalHoras { get; private set; }
        public decimal ValorTotal { get; private set; }
    }

    public class CotacaoValidator : AbstractValidator<Cotacao>
    {
        public CotacaoValidator()
        {
            RuleFor(cotacao => cotacao.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(cotacao => cotacao.VeiculoId).NotNull().NotEqual(Guid.Empty).WithMessage("O veículo não foi informado.");
            RuleFor(cotacao => cotacao.TotalHoras).GreaterThan(0).WithMessage("O total de horas não foi informado.");
            RuleFor(cotacao => cotacao.ValorTotal).GreaterThan(0).WithMessage("Não foi possível carregar o Valor total corretamente.");
        }
    }
}
