using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Infrastructure
{
    public interface IVeiculoRepository
    {
        Task<Veiculo> BuscarVeiculo(Guid id);

        Task<IEnumerable<Categoria>> BuscarTodosVeiculosPorCategoria();

        Task SalvarVeiculo(Veiculo veiculo);

        Task AtualizarVeiculo(Veiculo veiculo);
    }
}
