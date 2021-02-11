using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public class Modelo : DomainBase
    {
        public Modelo(Guid? id, string nome, Guid marcaId, DateTime? dataCriacao) : base(id, dataCriacao)
        {
            MarcaId = marcaId;
            DefinirOuAlterarNome(nome);
        }

        public Modelo(Guid? id, string nome, Guid marcaId, List<Veiculo> veiculos, DateTime? dataCriacao) : base(id, dataCriacao)
        {
            MarcaId = marcaId;
            DefinirOuAlterarNome(nome);
            AdicionarVeiculos(veiculos);
        }

        public string Nome { get; private set; }
        public Guid MarcaId { get; private set; }
        public List<Veiculo> Veiculos { get; private set; }

        public void DefinirOuAlterarNome(string nome)
        {
            Nome = nome;

            var resultadoValidacao = new ModeloValidator().Validate(this);
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

        public void ValidarMarca(Marca marca)
        {
            if (marca == null || marca.Id == Guid.Empty)
            {
                Valido = false;
                Erros.Add(new FluentValidation.Results.ValidationFailure("MarcaId", "A marca informada não existe."));
            }
        }
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
