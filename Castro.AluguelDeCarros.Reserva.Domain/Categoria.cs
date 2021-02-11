using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Categoria : DomainBase
    {
        public Categoria(Guid? id, string nome, DateTime? dataCriacao) : base(id, dataCriacao)
        {
            DefinirOuAlterarNome(nome);
        }

        public Categoria(Guid id, string nome, List<Veiculo> veiculos, DateTime dataCriacao) : base(id, dataCriacao)
        {
            DefinirOuAlterarNome(nome);
            AdicionarVeiculos(veiculos);
        }

        public string Nome { get; private set; }
        public List<Veiculo> Veiculos { get; private set; }


        #region Métodos
        public void DefinirOuAlterarNome(string nome)
        {
            Nome = nome;

            var resultadoValidacao = new CategoriaValidator().Validate(this);
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
        #endregion
    }

    public class CategoriaValidator : AbstractValidator<Categoria>
    {
        public CategoriaValidator()
        {
            RuleFor(categoria => categoria.Id).NotNull().NotEqual(Guid.Empty);
            RuleFor(categoria => categoria.Nome).NotNull().NotEmpty().WithMessage("O nome da Categoria não foi informado");
        }
    }
}
