using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Veiculo : DomainBase
    {
        public Veiculo(Guid? id, string placa, Guid modeloId, DateTime ano, decimal valorHora, CombustivelEnum combustivel,
            float limitePortaMalas, CategoriaEnum categoria, DateTime dataCriacao, DateTime? dataAlteracao)
        {
            if (!id.HasValue)
                Id = Guid.NewGuid();

            Placa = placa;
            ModeloId = modeloId;
            Ano = ano;
            ValorHora = valorHora;
            Combustivel = combustivel;
            LimitePortaMalas = limitePortaMalas;
            Categoria = categoria;
            DataCriacao = dataCriacao;
            DataAlteracao = dataAlteracao;

            var resultadoValidacao = new VeiculoValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros = resultadoValidacao.Errors;
            Valido = resultadoValidacao.IsValid;
        }

        public Guid Id { get; private set; }
        public string Placa { get; private set; }
        public Guid ModeloId { get; private set; }
        public DateTime Ano { get; private set; }
        public decimal ValorHora { get; private set; }
        public CombustivelEnum Combustivel { get; private set; }
        public float LimitePortaMalas { get; private set; }
        public CategoriaEnum Categoria { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAlteracao { get; private set; }
    }


    public class VeiculoValidator : AbstractValidator<Veiculo>
    {
        public VeiculoValidator()
        {
            RuleFor(reserva => reserva.Id).NotNull();
            RuleFor(reserva => reserva.Placa).NotNull().NotEmpty().WithMessage("A placa do veículo não foi informada.");
            RuleFor(reserva => reserva.ModeloId).NotNull().WithMessage("O modelo do veículo não foi informado.");
            RuleFor(reserva => reserva.Ano).LessThanOrEqualTo(DateTime.MinValue).WithMessage("O ano informado não é válido.");
            RuleFor(reserva => reserva.ValorHora).NotNull().GreaterThan(0).WithMessage("Não foi possível obter o valor da hora.");
            RuleFor(reserva => reserva.Combustivel).NotNull().WithMessage("Não foi possível obter o tipo de combustível.");
            RuleFor(reserva => reserva.LimitePortaMalas).NotNull().GreaterThan(0).WithMessage("Não foi possível obter o tipo de combustível.");
            RuleFor(reserva => reserva.Categoria).NotNull().WithMessage("Não foi possível obter a categoria.");
        }
    }
}
