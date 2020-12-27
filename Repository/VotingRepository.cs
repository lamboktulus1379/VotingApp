using Contracts;
using Entities;
using Entities.Extensions;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VotingRepository : RepositoryBase<Voting>, IVotingRepository
    {
        private readonly ISortHelper<Voting> _sortHelper;
        private readonly IDataShaper<Voting> _dataShaper;

        public VotingRepository(RepositoryContext repositoryContext, ISortHelper<Voting> sortHelper, IDataShaper<Voting> dataShaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }
        public void CreateVoting(Voting voting)
        {
            Create(voting);
        }

        public void DeleteVoting(Voting voting)
        {
            Delete(voting);
        }

        public PagedList<ShapedEntity> GetVotings(VotingParameters votingParameters)
        {
            var votings = FindAll();
            SearchByName(ref votings, votingParameters.Name);
            var sortedOwners = _sortHelper.ApplySort(votings, votingParameters.OrderBy);
            var shapedOwners = _dataShaper.ShapeData(sortedOwners, votingParameters.Fields);

            return PagedList<ShapedEntity>.ToPagedList(shapedOwners,
                votingParameters.PageNumber,
                votingParameters.PageSize);
        }

        private void SearchByName(ref IQueryable<Voting> votings, string votingName)
        {
            if (!votings.Any() || string.IsNullOrWhiteSpace(votingName))
                return;

            if (string.IsNullOrEmpty(votingName))
                return;

            votings = votings.Where(o => o.Name.ToLower().Contains(votingName.Trim().ToLower()));
        }

        public ShapedEntity GetVotingById(Guid votingId, string fields)
        {
            var voting = FindByCondition(voting => voting.Id.Equals(votingId))
                .FirstOrDefault();

            return _dataShaper.ShapeData(voting, fields);
        }

        public Voting GetVotingById(Guid votingId)
        {
            return FindByCondition(voting => voting.Id.Equals(votingId))
                .FirstOrDefault();
        }

        public Voting GetVotingWithDetails(Guid votingId)
        {
            return FindByCondition(voting => voting.Id.Equals(votingId))
                .FirstOrDefault();
        }

        public void UpdateVoting(Voting dbVoting, Voting voting)
        {
            dbVoting.Map(voting);
            Update(dbVoting);
        }
    }
}
