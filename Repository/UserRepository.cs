using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public UserRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
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

        public User GetUserById(Guid id)
        {
            return FindByCondition(user => user.Id.Equals(id)).FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }
    }
}
