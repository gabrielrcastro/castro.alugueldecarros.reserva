﻿using Dapper.Contrib.Extensions;
using System;

namespace Castro.AluguelDeCarros.Reserva.Infrastructure.DbModels
{
    public abstract class BaseDbModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
