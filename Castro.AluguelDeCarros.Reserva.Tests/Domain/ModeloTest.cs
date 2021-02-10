using Castro.AluguelDeCarros.Reserva.Domain;
using System;
using Xunit;

namespace Castro.AluguelDeCarros.Reserva.Tests.Domain
{
    public class ModeloTest
    {
        [Theory]
        [InlineData("ECOSPORT SE 1.5 FLEX AT")]
        [InlineData("ARGO HGT 1.8")]
        [InlineData("JEEP RENEGADE SPORT 1.8 AT")]
        [InlineData("GOL MSI 1.0")]
        public void Novo_Modelo_Valido_Test(string nome)
        {
            var marcaId = Guid.NewGuid();
            Modelo modelo = new Modelo(null, nome, marcaId, null);

            Assert.Equal(nome, modelo.Nome);
            Assert.Equal(marcaId, modelo.MarcaId);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", modelo.Id.ToString());
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", modelo.MarcaId.ToString());
            Assert.True(modelo.Valido);
            Assert.Empty(modelo.Erros);
        }

        [Theory]
        [InlineData("ECOSPORT SE 1.5 FLEX AT")]
        [InlineData("ARGO HGT 1.8")]
        [InlineData("JEEP RENEGADE SPORT 1.8 AT")]
        [InlineData("GOL MSI 1.0")]
        public void Carrega_Existente_Modelo_Valido_Test(string nome)
        {
            var id = Guid.NewGuid();
            var marcaId = Guid.NewGuid();
            Modelo modelo = new Modelo(id, nome, marcaId, DateTime.Now);

            Assert.Equal(nome, modelo.Nome);
            Assert.Equal(id, modelo.Id);
            Assert.Equal(marcaId, modelo.MarcaId);
            Assert.True(modelo.Valido);
            Assert.Empty(modelo.Erros);
        }

        [Theory]
        [InlineData("  ")]
        [InlineData("")]
        [InlineData(null)]
        public void Novo_Modelo_Nome_Invalido_Test(string nome)
        {
            Modelo modelo = new Modelo(null, nome, Guid.NewGuid(), null);

            Assert.False(modelo.Valido);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", modelo.Id.ToString());
            Assert.NotEmpty(modelo.Erros);
        }

        [Fact]
        public void Novo_Modelo_Marca_Invalido_Test()
        {
            var marcaId = Guid.Empty;
            Modelo modelo = new Modelo(null, "ECOSPORT SE 1.5 FLEX AT", marcaId, null);

            Assert.False(modelo.Valido);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", modelo.Id.ToString());
            Assert.Equal("00000000-0000-0000-0000-000000000000", modelo.MarcaId.ToString());
            Assert.NotEmpty(modelo.Erros);
        }
    }
}
