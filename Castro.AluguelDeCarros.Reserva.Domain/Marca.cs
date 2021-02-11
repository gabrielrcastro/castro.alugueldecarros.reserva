using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Marca : DomainBase
    {
        public Marca(Guid? id, string nome, DateTime? dataCriacao) : base(id, dataCriacao)
        {
            DefinirOuAlterarNome(nome);
        }

        public Marca(Guid? id, string nome, List<Veiculo> veiculos, DateTime? dataCriacao) : base(id, dataCriacao)
        {
            DefinirOuAlterarNome(nome);
            AdicionarVeiculos(veiculos);
        }

        public string Nome { get; private set; }
        public List<Veiculo> Veiculos { get; private set; }

        public void DefinirOuAlterarNome(string nome)
        {
            Nome = nome;

            var resultadoValidacao = new MarcaValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public void AdicionarVeiculos(List<Veiculo> veiculos)
        {
            Veiculos = veiculos;

            foreach (var veiculo in Veiculos.Where(c => !c.Valido))
            {
                Valido = false;
                Erros.AddRange(veiculo.Erros);
            }
        }
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
