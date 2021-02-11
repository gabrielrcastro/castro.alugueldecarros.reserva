using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> BuscarTodas();
        Task<Categoria> Obter(Guid id);
        Task<Categoria> Salvar(Guid? id, string nome);
    }
}
