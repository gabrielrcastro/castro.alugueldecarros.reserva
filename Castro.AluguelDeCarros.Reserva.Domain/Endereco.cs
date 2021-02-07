using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Endereco
    {
        public Endereco(string enderecoCep, string enderecoLogradouro, string enderecoNumero, string enderecoComplemento, string enderecoCidade, string enderecoEstado)
        {
            EnderecoCep = enderecoCep;
            EnderecoLogradouro = enderecoLogradouro;
            EnderecoNumero = enderecoNumero;
            EnderecoComplemento = enderecoComplemento;
            EnderecoCidade = enderecoCidade;
            EnderecoEstado = enderecoEstado;

            var resultadoValidacao = new EnderecoValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros = resultadoValidacao.Errors;
            Valido = resultadoValidacao.IsValid;
        }

        public string EnderecoCep { get; private set; }
        public string EnderecoLogradouro { get; private set; }
        public string EnderecoNumero { get; private set; }
        public string EnderecoComplemento { get; private set; }
        public string EnderecoCidade { get; private set; }
        public string EnderecoEstado { get; private set; }

        public bool Valido { get; private set; }
        public IList<ValidationFailure> Erros { get; private set; }
    }


    public class EnderecoValidator : AbstractValidator<Endereco>
    {
        public EnderecoValidator()
        {
            RuleFor(reserva => reserva.EnderecoCep).NotNull().WithMessage("O CEP do endereço não foi informado.");
            RuleFor(reserva => reserva.EnderecoLogradouro).NotNull().WithMessage("O logradouro do endereço não foi informado.");
            RuleFor(reserva => reserva.EnderecoNumero).NotNull().WithMessage("O número do endereço não foi informado.");
            RuleFor(reserva => reserva.EnderecoComplemento).NotNull().WithMessage("O complemento do endereço não foi informado.");
            RuleFor(reserva => reserva.EnderecoCidade).NotNull().WithMessage("A cidade do endereço não foi informada.");
            RuleFor(reserva => reserva.EnderecoComplemento).NotNull().WithMessage("O estado do endereço não foi informado.");
        }
    }
}
