using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Repository;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;

namespace MyVotingApp.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ITokenService _tokenService;
        private readonly ILoggerManager _logger;
        private readonly RepositoryContext _repositoryContext;

        public UserController(RepositoryContext repositoryContext, ILoggerManager logger, IRepositoryWrapper repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _logger = logger;
            _repositoryContext = repositoryContext;
        }
        [HttpGet, Route("test")]
        public IActionResult GetTest()
        {
            var user = _repositoryContext.Users.ToList();

            return Ok(user);
        }
        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            var user = _repository.User.GetUserById(id);
            return Ok(user);
        }

        [HttpPost("{idUser}/votes/{idVoting}")]
        public IActionResult UsersVotes(Guid idUser, Guid idVoting)
        {
            User user = _repository.User.GetUserById(idUser);
            Voting voting = _repository.Voting.GetVotingById(idVoting);
            List<Voting> listVotings = new List<Voting>();

            var vt = _repositoryContext.Users.Where(c => c.Id.Equals(idUser)).SelectMany(c => c.Votings).ToList();
            bool exist = false;
            user.Votings = vt;
            foreach (var item in vt)
            {
                Console.WriteLine(vt);
                if (item.Id == idVoting)
                {
                    voting = item;
                    if (voting.VotersCount > 0)
                    {
                        voting.VotersCount -= 1;
                    }
                    exist = true;
                }

            }

            if (!exist)
            {
                voting.VotersCount += 1;
                listVotings.Add(voting);
                user.Votings = listVotings;
                _repositoryContext.UpdateRange(user);
            }
            var dbVoting = _repository.Voting.GetVotingById(idVoting);
            _repositoryContext.Votings.Update(voting);

            _repositoryContext.SaveChanges();

            return Ok();
        }
    }
}
