﻿namespace Nerdstore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}