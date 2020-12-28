using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {

        }

        public User CheckUser(string email, string password)
        {
            return FindByCondition(user => user.Email.Equals(email) && user.Password.Equals(password)).FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public User GetUserByEmail(string email)
        {
            return FindByCondition(user => user.Email.Equals(email)).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }
    }
}
