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
            Nome = nome;
            MarcaId = marcaId;

            var resultadoValidacao = new ModeloValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros = resultadoValidacao.Errors;
            Valido = resultadoValidacao.IsValid;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public Guid MarcaId { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAlteracao { get; private set; }
    }


    public class ModeloValidator : AbstractValidator<Modelo>
    {
        public ModeloValidator()
        {
            RuleFor(reserva => reserva.Id).NotNull();
            RuleFor(reserva => reserva.Nome).NotNull().WithMessage("O nome do Modelo não foi informado.");
            RuleFor(reserva => reserva.Nome).NotNull().WithMessage("A Marca do Modelo não foi informada.");
        }
    }
}
