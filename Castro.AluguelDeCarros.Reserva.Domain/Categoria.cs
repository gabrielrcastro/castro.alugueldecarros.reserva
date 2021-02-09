using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Categoria : DomainBase
    {
        public Categoria(Guid? id, string nome)
        {
            if (!id.HasValue)
                Id = Guid.NewGuid();
            else
                Id = id.Value;

            Nome = nome;

            var resultadoValidacao = new CategoriaValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;
        }

        public Categoria(Guid id, string nome, List<Veiculo> veiculos)
        {
            Id = id;
            Nome = nome;

            var resultadoValidacao = new CategoriaValidator().Validate(this);
            if (!resultadoValidacao.IsValid)
                Erros.AddRange(resultadoValidacao.Errors);
            Valido = resultadoValidacao.IsValid;

            AdicionarVeiculos(veiculos);
        }

        public string Nome { get; private set; }
        public List<Veiculo> Veiculos { get; private set; }


        #region Métodos
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
