using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class VotingExtensions
    {
        public static void Map(this Voting dbVoting, Voting voting)
        {
            dbVoting.Name = voting.Name;
            dbVoting.VotersCount = voting.VotersCount;
            dbVoting.Categories = voting.Categories;
            dbVoting.DateCreated = voting.DateCreated;
            dbVoting.DueDate = voting.DueDate;
            dbVoting.Description = voting.Description;
        }
    }
}       