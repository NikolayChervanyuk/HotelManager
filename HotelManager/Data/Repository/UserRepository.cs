using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private HmDbContext context;
        public UserRepository(HmDbContext context)
        {
            this.context = context;
        }
        public IQueryable<User> GetUsers()
        {
            return context.Users;
        }
    }
}
