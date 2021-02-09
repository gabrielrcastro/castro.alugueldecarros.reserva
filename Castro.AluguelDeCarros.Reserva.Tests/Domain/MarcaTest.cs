using Castro.AluguelDeCarros.Reserva.Domain;
using System;
using Xunit;

namespace Castro.AluguelDeCarros.Reserva.Tests.Domain
{
    public class MarcaTest
    {
        [Theory]
        [InlineData("FIAT")]
        [InlineData("FORD")]
        [InlineData("CHEVROLET")]
        [InlineData("VOLKSWAGEN")]
        public void Nova_Marca_Valido_Test(string nome)
        {
            Marca marca = new Marca(null, nome);

            Assert.Equal(nome, marca.Nome);
            Assert.NotEqual("00000000-0000-0000-0000-000000000000", marca.Id.ToString());
            Assert.True(marca.Valido);
            Assert.Empty(marca.Erros);
        }

        [Theory]
        [InlineData("FIAT")]
        [InlineData("FORD")]
        [InlineData("CHEVROLET")]
        [InlineData("VOLKSWAGEN")]
        public void Carrega_Existente_Marca_Valido_Test(string nome)
        {
            var id = Guid.NewGuid();
            Marca marca = new Marca(id, nome);

            Assert.Equal(nome, marca.Nome);
            Assert.Equal(id, marca.Id);
            Assert.True(marca.Valido);
            Assert.Empty(marca.Erros);
        }

        [Fact]
        public void Nova_Marca_Invalido_Test()
        {
            Marca marca = new Marca(null, null);

            Assert.Null(marca.Nome);
            Assert.False(marca.Valido);
            Assert.NotEmpty(marca.Erros);
        }
    }
}
