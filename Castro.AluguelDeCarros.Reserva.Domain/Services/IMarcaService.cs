using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Castro.AluguelDeCarros.Reserva.Domain.Services
{
    public interface IMarcaService
    {
        Task<IEnumerable<Marca>> BuscarTodas();
        Task<Marca> Obter(Guid id);
        Task<Marca> Salvar(Guid? id, string nome);
    }
}
