using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using System;

namespace Castro.AluguelDeCarros.Reserva.Domain.Models
{
    public class VeiculoModel
    {
        public string Placa { get; set; }
        public Guid ModeloId { get; set; }
        public int Ano { get; set; }
        public decimal ValorHora { get; set; }
        public CombustivelEnum TipoCombustivel { get; set; }
        public float LimitePortaMalas { get; set; }
        public Guid CategoriaId { get; set; }
    }
}
