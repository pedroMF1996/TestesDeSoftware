﻿using Nerdstore.Core.DomainObjects;

namespace Nerdstore.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}