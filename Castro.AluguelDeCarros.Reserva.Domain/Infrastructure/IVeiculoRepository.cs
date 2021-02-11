using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Infrastructure
{
    public interface IVeiculoRepository
    {
        Task<Veiculo> BuscarVeiculo(Guid id);
        Task<IEnumerable<Veiculo>> BuscarTodosVeiculos();
        Task SalvarVeiculo(Veiculo veiculo);
        Task AtualizarVeiculo(Veiculo veiculo);
        Task<IEnumerable<Categoria>> BuscarTodosVeiculosPorCategoria();
        Task<IEnumerable<Marca>> BuscarVeiculosPorMarca(Guid id);
        Task<IEnumerable<Modelo>> BuscarVeiculosPorModelo(Guid id);
    }
}
