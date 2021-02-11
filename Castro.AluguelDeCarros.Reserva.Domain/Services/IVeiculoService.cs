using Castro.AluguelDeCarros.Reserva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Services
{
    public interface IVeiculoService
    {
        Task<IEnumerable<Veiculo>> BuscarTodos();
        Task<Veiculo> Obter(Guid id);
        Task<Veiculo> Salvar(Guid? id, VeiculoModel model);
        Task<IEnumerable<Categoria>> BuscarVeiculosPorCategoria();
        Task<IEnumerable<Marca>> BuscarVeiculosPorMarca(Guid id);
        Task<IEnumerable<Modelo>> BuscarVeiculosPorModelo(Guid id);
    }
}
