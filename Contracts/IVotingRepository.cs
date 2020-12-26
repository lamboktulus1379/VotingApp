using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IVotingRepository : IRepositoryBase<Voting>
    {
		PagedList<ShapedEntity> GetVotings(VotingParameters votingParameters);
		ShapedEntity GetVotingById(Guid votingId, string fields);
		Voting GetVotingWithDetails(Guid votingId);
		Voting GetVotingById(Guid votingId);

		void CreateVoting(Voting voting);
		void UpdateVoting(Voting dbVoting, Voting voting);
		void DeleteVoting(Voting voting);
	}
}
