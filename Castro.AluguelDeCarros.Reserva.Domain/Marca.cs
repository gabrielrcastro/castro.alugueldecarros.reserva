using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Marca : DomainBase
    {
        public Marca(Guid? id, string nome)
        {
            if (!id.HasValue)
                Id = Guid.NewGuid();
            Nome = nome;

            var resultadoValidacao = new MarcaValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public string Nome { get; private set; }
    }


    public class MarcaValidator : AbstractValidator<Marca>
    {
        public MarcaValidator()
        {
            RuleFor(reserva => reserva.Id).NotNull();
            RuleFor(reserva => reserva.Nome).NotNull().WithMessage("O nome da Marca não foi informado");
        }
    }
}
