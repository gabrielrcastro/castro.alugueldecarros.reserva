using FluentValidation.Results;
using System.Collections.Generic;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public abstract class DomainBase
    {
        public bool Valido { get; protected set; }
        public IList<ValidationFailure> Erros { get; protected set; }
    }
}
