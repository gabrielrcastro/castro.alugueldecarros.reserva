using FluentValidation;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public abstract class UsuarioBase : DomainBase
    {
        public UsuarioBase(string login, string senha)
        {
            Login = login;
            Senha = senha;

            var resultadoValidacao = new UsuarioValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public string Login { get; private set; }
        public string Senha { get; private set; }
    }

    public class UsuarioValidator : AbstractValidator<UsuarioBase>
    {
        public UsuarioValidator()
        {
            RuleFor(reserva => reserva.Login).NotNull().NotEmpty().WithMessage("O login do usuário não foi informado.");
            RuleFor(reserva => reserva.Senha).NotNull().NotEmpty().WithMessage("A senha do usuário não foi informada.");
        }
    }
}
