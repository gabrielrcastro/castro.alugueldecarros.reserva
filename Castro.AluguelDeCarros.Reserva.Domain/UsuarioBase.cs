using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using FluentValidation;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public abstract class UsuarioBase : DomainBase
    {
        public UsuarioBase(Guid? id, string login, string senha, string nome, DateTime? dataCriacao) : base(id, dataCriacao)
        {
            Nome = nome;
            Login = login;
            Senha = senha;

            var resultadoValidacao = new UsuarioValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public string Login { get; private set; }
        public string Senha { get; private set; }
        public TipoUsuarioEnum Tipo { get; protected set; }
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; protected set; }
        public Endereco Endereco { get; protected set; }
        public string Cpf { get; protected set; }
        public string Matricula { get; protected set; }
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
