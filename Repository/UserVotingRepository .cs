using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserVotingRepository : RepositoryBase<UsersVotings>, IUserVotingRepository
    {
        public UserVotingRepository(RepositoryContext repositoryContext): base(repositoryContext)
        {

        }

        public void CreateUsersVoting(UsersVotings usersVotings)
        {
            Create(usersVotings);
        }

        public void DeleteUsersVoting(UsersVotings usersVotings)
        {
            throw new NotImplementedException();
        }

        public void UpdateUsersVoting(UsersVotings dbUsersVoting, UsersVotings usersVotings)
        {
            throw new NotImplementedException();
        }
    }
}
