namespace Castro.AluguelDeCarros.Reserva.Domain.Models
{
    public class VeiculoModel
    {
        public string Placa { get; set; }
        public string ModeloNome { get; set; }
        public string Ano { get; set; }
        public decimal ValorHora { get; set; }
        public string TipoCombustivel { get; set; }
        public float LimitePortaMalas { get; set; }
        public string Categoria { get; set; }
    }
}
