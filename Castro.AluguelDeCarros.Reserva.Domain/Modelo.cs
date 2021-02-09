using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Modelo : DomainBase
    {
        public Modelo(Guid? id, string nome, Guid marcaId)
        {
            if (!id.HasValue)
                Id = Guid.NewGuid();
            else
                Id = id.Value;

            Nome = nome;
            MarcaId = marcaId;

            var resultadoValidacao = new ModeloValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public string Nome { get; private set; }
        public Guid MarcaId { get; private set; }
    }


    public class ModeloValidator : AbstractValidator<Modelo>
    {
        public ModeloValidator()
        {
            RuleFor(reserva => reserva.Id).NotNull();
            RuleFor(reserva => reserva.Nome).NotNull().NotEmpty().WithMessage("O nome do Modelo não foi informado.");
            RuleFor(reserva => reserva.MarcaId).NotNull().NotEqual(Guid.Empty).WithMessage("A Marca do Modelo não foi informada.");
        }
    }
}
