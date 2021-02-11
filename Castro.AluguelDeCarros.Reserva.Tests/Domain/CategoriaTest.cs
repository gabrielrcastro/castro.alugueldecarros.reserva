using Castro.AluguelDeCarros.Reserva.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace Castro.AluguelDeCarros.Reserva.Tests.Domain
{
    public class CategoriaTest
    {
        [Theory]
        [InlineData("Luxo")]
        [InlineData("Básico")]
        [InlineData("Completo")]
        public void Nova_Categoria_Valida_Test(string nome)
        {
            Categoria categoria = new Categoria(null, nome, null);

            Assert.Equal(nome, categoria.Nome);
            Assert.NotEqual(Guid.Empty, categoria.Id);
            Assert.True(categoria.Valido);
            Assert.Empty(categoria.Erros);
        }

        [Theory]
        [InlineData("Luxo")]
        [InlineData("Básico")]
        [InlineData("Completo")]
        public void Carrega_Existente_Categoria_Valida_Test(string nome)
        {
            var id = Guid.NewGuid();
            Categoria categoria = new Categoria(id, nome, DateTime.Now);

            Assert.Equal(nome, categoria.Nome);
            Assert.Equal(id, categoria.Id);
            Assert.True(categoria.Valido);
            Assert.Empty(categoria.Erros);
        }

        [Fact]
        public void Carrega_Existente_Categoria_ComVeiculos_Valida_Test()
        {
            var id = Guid.NewGuid();
            var veiculos = new List<Veiculo>
            {
                new VeiculoTest().ObterVeiculoValido(),
                new VeiculoTest().ObterVeiculoValido()
            };

            Categoria categoria = new Categoria(id, "Luxo", veiculos, DateTime.Now);

            Assert.Equal(id, categoria.Id);
            Assert.True(categoria.Valido);
            Assert.Empty(categoria.Erros);
        }

        [Fact]
        public void Carrega_Existente_Categoria_ComVeiculos_Invalida_Test()
        {
            var id = Guid.NewGuid();
            var veiculos = new List<Veiculo>
            {
                new VeiculoTest().ObterVeiculoInvalido(),
                new VeiculoTest().ObterVeiculoValido()
            };

            Categoria categoria = new Categoria(id, "Luxo", veiculos, DateTime.Now);

            Assert.Equal(id, categoria.Id);
            Assert.False(categoria.Valido);
            Assert.NotEmpty(categoria.Erros);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData(null)]
        public void Nova_Categoria_Nome_Invalido_Test(string nome)
        {
            Categoria categoria = new Categoria(null, nome, null);

            Assert.False(categoria.Valido);
            Assert.NotEqual(Guid.Empty, categoria.Id);
            Assert.NotEmpty(categoria.Erros);
        }

        [Theory]
        [InlineData("Luxo")]
        [InlineData("Básico")]
        [InlineData("Completo")]
        public void DefinirAlterarNome_Valido_Test(string nome)
        {
            Categoria categoria = new Categoria(Guid.NewGuid(), "NOME ANTIGO", DateTime.Now);
            categoria.DefinirOuAlterarNome(nome);

            Assert.Equal(nome, categoria.Nome);
            Assert.NotEqual(Guid.Empty, categoria.Id);
            Assert.True(categoria.Valido);
            Assert.Empty(categoria.Erros);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData(null)]
        public void DefinirAlterarNome_Invalido_Test(string nome)
        {
            Categoria categoria = new Categoria(Guid.NewGuid(), "NOME ANTIGO", DateTime.Now);
            categoria.DefinirOuAlterarNome(nome);

            Assert.Equal(nome, categoria.Nome);
            Assert.NotEqual(Guid.Empty, categoria.Id);
            Assert.False(categoria.Valido);
            Assert.NotEmpty(categoria.Erros);
        }
    }
}
