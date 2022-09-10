using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    public Task<IReadOnlyList<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}