using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Veiculo : DomainBase
    {
        public Veiculo(Guid? id, string placa, Guid modeloId, DateTime ano, decimal valorHora, CombustivelEnum combustivel,
            float limitePortaMalas, Guid categoriaId, DateTime dataCriacao, DateTime? dataAlteracao)
        {
            if (!id.HasValue)
                Id = Guid.NewGuid();
            else
                Id = id.Value;

            Placa = placa;
            ModeloId = modeloId;
            Ano = ano;
            ValorHora = valorHora;
            Combustivel = combustivel;
            LimitePortaMalas = limitePortaMalas;
            CategoriaId = categoriaId;
            DataCriacao = dataCriacao;
            DataAlteracao = dataAlteracao;

            var resultadoValidacao = new VeiculoValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public string Placa { get; private set; }
        public Guid ModeloId { get; private set; }
        public DateTime Ano { get; private set; }
        public decimal ValorHora { get; private set; }
        public CombustivelEnum Combustivel { get; private set; }
        public float LimitePortaMalas { get; private set; }
        public Guid CategoriaId { get; private set; }
    }


    public class VeiculoValidator : AbstractValidator<Veiculo>
    {
        public VeiculoValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(c => c.Placa).NotNull().NotEmpty().Length(7).WithMessage("A placa do veículo não foi informada.");
            RuleFor(c => c.ModeloId).NotNull().NotEqual(Guid.Empty).WithMessage("O modelo do veículo não foi informado.");
            RuleFor(c => c.Ano).GreaterThan(DateTime.MinValue).WithMessage("O ano informado não é válido.");
            RuleFor(c => c.ValorHora).NotNull().GreaterThan(0).WithMessage("Não foi possível obter o valor da hora.");
            RuleFor(c => c.Combustivel).NotNull().WithMessage("Não foi possível obter o tipo de combustível.");
            RuleFor(c => c.LimitePortaMalas).NotNull().GreaterThan(0).WithMessage("Não foi possível obter o limite do porta malas.");
            RuleFor(c => c.CategoriaId).NotNull().NotEqual(Guid.Empty).WithMessage("Não foi possível obter a categoria.");
        }
    }
}
