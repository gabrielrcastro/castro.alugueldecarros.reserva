using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Autenticacao : DomainBase
    {
        public Autenticacao(UsuarioBase usuario) : base(null, null)
        {
            if (usuario != null && usuario.Valido)
            {
                Login = usuario.Login;
                UsuarioId = usuario.Id;
                CriarAutorizacaoAsync(usuario.Tipo);
            }

            var resultadoValidacao = new AutenticacaoValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);

            Valido = resultadoValidacao.IsValid;
        }

        public string Login { get; private set; }
        public Guid UsuarioId { get; private set; }
        public string TokenAcesso { get; private set; }

        private void CriarAutorizacaoAsync(TipoUsuarioEnum tipoUsuario)
        {
            var secret = Encoding.ASCII.GetBytes("57a1f196-7e43-4709-bf4f-e86321c869eb");
            var criadoEm = DateTime.UtcNow;
            DateTime? expiraEm = criadoEm.AddSeconds(7200);
            var tokenHandler = new JwtSecurityTokenHandler { };

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim> {
                    new Claim("Login", Login),
                    new Claim(ClaimTypes.NameIdentifier, UsuarioId.ToString()),
                    new Claim(ClaimTypes.Role, ((int)tipoUsuario).ToString())
                }),
                Expires = expiraEm,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            });

            TokenAcesso = tokenHandler.WriteToken(token);
        }
    }

    public class AutenticacaoValidator : AbstractValidator<Autenticacao>
    {
        public AutenticacaoValidator()
        {
            RuleFor(reserva => reserva.TokenAcesso).NotNull().WithMessage("Usuário ou senha inválidos.");
        }
    }
}
