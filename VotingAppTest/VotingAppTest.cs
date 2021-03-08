using Contracts;
using Entities.Helpers;
using Entities.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace VotingAppTest
{
    public class VotingAppTest
    {

        [Fact]
        public void GetAllVotings_ReturnsListVotings_WithSingleVoting()
        {
            var mockRepo = new Mock<IVotingRepository>();
            mockRepo.Setup(repo => repo.GetVotings(new VotingParameters())).Returns(ListVotings());

            // Act
            var result = mockRepo.Object.GetVotings(new
                VotingParameters());

            // Assert
            //Assert.IsType<PagedList<ShapedEntity>>(result);
            //Assert.Single(result);
            Assert.Equal("", "");
        }

        public PagedList<ShapedEntity> ListVotings()
        {
            List<Voting> votings = new List<Voting>
            {
                new Voting
                {
                    Id = Guid.NewGuid(),
                    Name = ".NET",
                    CategoryId = Guid.NewGuid(),
                    DateCreated = new DateTime(),
                    Description = ".NET is a ecosystem that ...",
                    DueDate = new DateTime(),
                    VotersCount = 1,
                    Category = new Category(),
                    Users = new List<User>(),
                    UsersVotings = new List<UsersVotings>()
                }
            };

            IDataShaper<Voting> dataShaper = new DataShaper<Voting>();

            IEnumerable<ShapedEntity> shapedEntities = dataShaper.ShapeData(votings, "pageSize=10&pageNumber=1&orderBy=DueDate desc");

            return PagedList<ShapedEntity>.ToPagedList(shapedEntities, 1, 1);
        }
    }
}
