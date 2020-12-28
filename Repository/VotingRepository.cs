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
        private readonly RepositoryContext _repositoryContext;

        public VotingRepository(RepositoryContext repositoryContext, ISortHelper<Voting> sortHelper, IDataShaper<Voting> dataShaper) : base(repositoryContext)
        {
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
            _repositoryContext = repositoryContext;
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
            IQueryable<Voting> votings;
            if (votingParameters.WhereIn != null)
            {
            string[] filterCategories = votingParameters?.WhereIn.Trim().Split(",");
                votings = FindAll().Where(e => filterCategories.Contains(e.CategoryId.ToString())).Include(ct => ct.Category).Include(u => u.Users).AsNoTracking();

            } else
            {
                votings = FindAll().Include(ct => ct.Category).AsNoTracking();
            }
            SearchByName(ref votings, votingParameters.Name);
            var sortedVotings = _sortHelper.ApplySort(votings, votingParameters.OrderBy);
            var shapedVotings = _dataShaper.ShapeData(sortedVotings, votingParameters.Fields);

            return PagedList<ShapedEntity>.ToPagedList(shapedVotings,
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
                .Include(ac => ac.Category)
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
                .Include(ct => ct.Category)
                .FirstOrDefault();
        }

        public void UpdateVoting(Voting dbVoting, Voting voting)
        {
            dbVoting.Map(voting);
            Update(dbVoting);
        }
    }
}
