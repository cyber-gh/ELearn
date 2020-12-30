using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELearn.Application.Generic
{
    public interface ICrud<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> Create(T value);
        public Task<T?> Read(Guid idx);
        public Task<T> Update(Guid idx, T value);
        public Task Delete(Guid idx);
    }
}