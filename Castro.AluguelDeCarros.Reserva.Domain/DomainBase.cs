using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Castro.AluguelDeCarros.Reserva.Domain
{
    public abstract class DomainBase
    {
        public DomainBase()
        {
            Erros = new List<ValidationFailure>();
            Valido = false;
        }

        public DomainBase(Guid? id, DateTime? dataCriacao)
        {
            if (!id.HasValue)
            {
                DataCriacao = DateTime.Now;
                Id = Guid.NewGuid();
            }
            else
            {
                DataCriacao = dataCriacao ?? DateTime.Now;
                Id = id.Value;
            }

            Erros = new List<ValidationFailure>();
            Valido = false;
        }

        public Guid Id { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime? DataAlteracao { get; protected set; }
        public bool Valido { get; protected set; }
        public List<ValidationFailure> Erros { get; protected set; }
    }
}
