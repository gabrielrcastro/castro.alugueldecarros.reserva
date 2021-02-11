using Castro.AluguelDeCarros.Reserva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Services
{
    public interface IModeloService
    {
        Task<IEnumerable<Modelo>> BuscarTodos();
        Task<Modelo> Obter(Guid id);
        Task<Modelo> Salvar(Guid? id, ModeloModel nome);
    }
}
