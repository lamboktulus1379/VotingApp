using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User CheckUser(string email, string password);

        User GetUserByEmail(string email);
        void UpdateUser(User user);
        void CreateUser(User user);
    }
}
