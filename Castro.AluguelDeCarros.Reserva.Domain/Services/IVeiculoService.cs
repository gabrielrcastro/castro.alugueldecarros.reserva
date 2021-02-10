using Castro.AluguelDeCarros.Reserva.Domain.Enums;
using Castro.AluguelDeCarros.Reserva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Services
{
    public interface IVeiculoService
    {
        Task<IEnumerable<Categoria>> BuscarVeiculosPorCategoria();
        Task<Veiculo> Obter(Guid id);
        Task<Veiculo> Salvar(Guid? id, VeiculoModel model);
    }
}
