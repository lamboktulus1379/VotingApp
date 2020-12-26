using MyVotingApp.Filters;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Extensions;
using Entities.Helpers;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyVotingApp.Controllers
{
    [Route("api/votings")]
    [ApiController]
    public class VotingController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        private LinkGenerator _linkGenerator;
        public VotingController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }
        [HttpGet]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public IActionResult GetVotings([FromQuery] VotingParameters votingParameters)
        {
            var votings = _repository.Voting.GetVotings(votingParameters);

            var metadata = new
            {
                votings.TotalCount,
                votings.PageSize,
                votings.CurrentPage,
                votings.TotalPages,
                votings.HasNext,
                votings.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            _logger.LogInfo($"Returned {votings.TotalCount} votings from database.");

            var shapedVotings = votings.Select(o => o.Entity).ToList();
            var mediaType = (MediaTypeHeaderValue)HttpContext.Items["AcceptHeaderMediaType"];
            if (!mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
            {
                return Ok(shapedVotings);
            }
            for (var index = 0; index < votings.Count; index++)
            {
                var votingLinks = CreateLinksForVoting(votings[index].Id, votingParameters.Fields);
                shapedVotings[index].Add("Links", votingLinks);
            }
            var votingsWrapper = new LinkCollectionWrapper<Entity>(shapedVotings);
            return Ok(CreateLinksForVotings(votingsWrapper));
        }

        [HttpPost]
        public IActionResult CreateVoting([FromBody] Voting voting)
        {
            if (voting.IsObjectNull())
            {
                _logger.LogError("Voting object sent from client is null.");
                return BadRequest("Voting object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid voting object sent from client.");
                return BadRequest("Invalid model object");
            }

            _repository.Voting.CreateVoting(voting);
            _repository.Save();

            return CreatedAtRoute("VotingById", new { id = voting.Id }, voting);
        }

        [HttpGet("{id}", Name = "VotingById")]
        public IActionResult GetVotingById(Guid id, [FromQuery] string fields)
        {
            var voting = _repository.Voting.GetVotingById(id, fields);

            if (voting.Id == Guid.Empty)
            {
                _logger.LogError($"Voting with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            var mediaType = (MediaTypeHeaderValue)HttpContext.Items["AcceptHeaderMediaType"];

            if (!mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogInfo($"Returned shaped voting with id: {id}");
                return Ok(voting.Entity);
            }

            voting.Entity.Add("Links", CreateLinksForVoting(voting.Id, fields));

            return Ok(voting.Entity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVoting(Guid id, [FromBody] Voting voting)
        {
            if (voting.IsObjectNull())
            {
                _logger.LogError("Voting object sent from client is null.");
                return BadRequest("Voting object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid voting object sent from client.");
                return BadRequest("Invalid model object");
            }

            var dbVoting = _repository.Voting.GetVotingById(id);
            if (dbVoting.IsEmptyObject())
            {
                _logger.LogError($"Voting with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            _repository.Voting.UpdateVoting(dbVoting, voting);
            _repository.Save();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVoting(Guid id)
        {
            var voting = _repository.Voting.GetVotingById(id);
            if (voting.IsEmptyObject())
            {
                _logger.LogError($"Voting with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            _repository.Voting.DeleteVoting(voting);
            _repository.Save();

            return NoContent();
        }

        private IEnumerable<Link> CreateLinksForVoting(Guid id, string fields)
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetVotingById), values: new {id, fields}),
                "self",
                "GET"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteVoting), values: new {id}),
                "delete_voting",
                "DELETE"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateVoting), values: new {id}),
                "update_voting",
                "PUT")
            };

            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForVotings(LinkCollectionWrapper<Entity> votingsWrapper)
        {
            votingsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetVotings), values: new { }),
                    "self",
                    "GET"));

            return votingsWrapper;
        }
    }
}
