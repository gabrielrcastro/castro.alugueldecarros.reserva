using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Infrastructure
{
    public interface ICategoriaRepository
    {
        Task<Categoria> BuscarCategoria(Guid id);

        Task<IEnumerable<Categoria>> BuscarTodasCategorias();

        Task SalvarCategoria(Categoria categoria);

        Task AtualizarCategoria(Categoria categoria);
    }
}
