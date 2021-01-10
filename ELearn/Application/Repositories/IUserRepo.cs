using System;
using System.Threading.Tasks;
using ELearn.Domain;

namespace ELearn.Application.Repositories
{
    public interface IUserRepo
    {
        Task<AppUser?> GetUser(Guid idx);
    }
}