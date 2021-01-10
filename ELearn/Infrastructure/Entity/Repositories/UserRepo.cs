using System;
using System.Linq;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using Microsoft.EntityFrameworkCore;

namespace ELearn.Infrastructure.Entity.Repositories
{
    public class UserRepo: IUserRepo
    {

        private readonly AuthContext _authContext;

        public UserRepo(AuthContext authContext)
        {
            _authContext = authContext;
        }

        public async Task<AppUser?> GetUser(Guid idx)
        {
            var user = await _authContext.Users.FirstOrDefaultAsync(it => it.Id == idx.ToString());

            return user?.ToModel();
        }
    }
}