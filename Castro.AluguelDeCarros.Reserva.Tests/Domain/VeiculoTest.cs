using Castro.AluguelDeCarros.Reserva.Domain;
using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using System;
using System.Linq;
using Xunit;

namespace Castro.AluguelDeCarros.Reserva.Tests.Domain
{
    public class VeiculoTest
    {
        [Fact]
        public void Novo_Veiculo_Valido_Test()
        {
            Veiculo veiculo = ObterVeiculoValido();

            Assert.NotEqual(Guid.Empty, veiculo.Id);
            Assert.True(veiculo.Valido);
            Assert.Empty(veiculo.Erros);
        }

        [Fact]
        public void Novo_Veiculo_Invalido_Test()
        {
            Veiculo veiculo = ObterVeiculoInvalido();

            Assert.False(veiculo.Valido);
            Assert.NotNull(veiculo.Erros);
            Assert.NotEmpty(veiculo.Erros);
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.Placa));
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.Ano));
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.CategoriaId));
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.ModeloId));
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.LimitePortaMalas));
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.Id));
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.ValorHora));
        }

        [Theory]
        [InlineData("PLK2021", 2021)]
        [InlineData("PNA2020", 2020)]
        [InlineData("GHH2015", 2015)]
        [InlineData("GOL1010", 2010)]
        public void Carrega_Existente_Veiculo_Valido_Test(string placa, int ano)
        {
            var id = Guid.NewGuid();
            Veiculo veiculo = new Veiculo(id, placa, Guid.NewGuid(), new DateTime(ano, 1, 1), 159.92m, CombustivelEnum.Gasolina, 20.4f, Guid.NewGuid(), DateTime.Now, null);

            Assert.Equal(id, veiculo.Id);
            Assert.NotEqual(Guid.Empty, veiculo.Id);
            Assert.Equal(placa, veiculo.Placa);
            Assert.Equal(ano, veiculo.Ano.Year);
            Assert.True(veiculo.Valido);
            Assert.Empty(veiculo.Erros);
        }

        [Theory]
        [InlineData("PLK100")]
        [InlineData("PLK10101")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Novo_Veiculo_Placa_Invalido_Test(string placa)
        {
            Veiculo veiculo = new Veiculo(null, placa, Guid.NewGuid(), new DateTime(2015, 1, 1), 159.92m, CombustivelEnum.Gasolina, 20.4f, Guid.NewGuid(), DateTime.Now, null);

            Assert.False(veiculo.Valido);
            Assert.NotEmpty(veiculo.Erros);
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.Placa));
        }

        [Fact]
        public void Novo_Veiculo_Modelo_Invalido_Test()
        {
            Veiculo veiculo = new Veiculo(null, "HLN1029", Guid.Empty, new DateTime(2015, 1, 1), 159.92m, CombustivelEnum.Gasolina, 20.4f, Guid.NewGuid(), DateTime.Now, null);

            Assert.False(veiculo.Valido);
            Assert.NotEmpty(veiculo.Erros);
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.ModeloId));
        }

        [Fact]
        public void Novo_Veiculo_Ano_Invalido_Test()
        {
            Veiculo veiculo = new Veiculo(null, "HLN1029", Guid.NewGuid(), DateTime.MinValue, 159.92m, CombustivelEnum.Gasolina, 20.4f, Guid.NewGuid(), DateTime.Now, null);

            Assert.False(veiculo.Valido);
            Assert.NotEmpty(veiculo.Erros);
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.Ano));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10.22)]
        public void Novo_Veiculo_ValorHora_Invalido_Test(decimal valorHora)
        {
            Veiculo veiculo = new Veiculo(null, "HLN1029", Guid.NewGuid(), new DateTime(2015, 1, 1), valorHora, CombustivelEnum.Gasolina, 20.4f, Guid.NewGuid(), DateTime.Now, null);

            Assert.False(veiculo.Valido);
            Assert.NotEmpty(veiculo.Erros);
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.ValorHora));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10.22)]
        public void Novo_Veiculo_LimitePortaMalas_Invalido_Test(float limitePortaMalas)
        {
            Veiculo veiculo = new Veiculo(null, "HLN1029", Guid.NewGuid(), new DateTime(2015, 1, 1), 120.54m, CombustivelEnum.Gasolina, limitePortaMalas, Guid.NewGuid(), DateTime.Now, null);

            Assert.False(veiculo.Valido);
            Assert.NotEmpty(veiculo.Erros);
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.LimitePortaMalas));
        }

        [Fact]
        public void Novo_Veiculo_Categoria_Invalido_Test()
        {
            Veiculo veiculo = new Veiculo(null, "HLN1029", Guid.NewGuid(), new DateTime(2015, 1, 1), 120.54m, CombustivelEnum.Gasolina, 13.7f, Guid.Empty, DateTime.Now, null);

            Assert.False(veiculo.Valido);
            Assert.NotEmpty(veiculo.Erros);
            Assert.Contains(veiculo.Erros, c => c.PropertyName == nameof(Veiculo.CategoriaId));
        }

        public Veiculo ObterVeiculoValido()
        {
            return new Veiculo(null, "HLN1029", Guid.NewGuid(), new DateTime(2010, 1, 1), 159.92m, CombustivelEnum.Gasolina, 20.4f, Guid.NewGuid(), DateTime.Now, null);
        }

        public Veiculo ObterVeiculoInvalido()
        {
            return new Veiculo(Guid.Empty, null, Guid.Empty, DateTime.MinValue, 0, CombustivelEnum.Gasolina, 0, Guid.Empty, DateTime.MinValue, null);
        }
    }
}
