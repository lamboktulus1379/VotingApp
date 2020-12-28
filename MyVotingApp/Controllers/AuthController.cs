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

namespace MyVotingApp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ITokenService _tokenService;
        private readonly ILoggerManager _logger;

        public AuthController(ILoggerManager logger, IRepositoryWrapper repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
            _logger = logger;
        }
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] Login dbUser)
        {
            if (dbUser == null)
            {
                return BadRequest("Invalid client request");
            }
            User user = _repository.User.CheckUser(dbUser.Email, dbUser.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dbUser.Email),
                new Claim(ClaimTypes.Role, "Owner")
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.Now.AddDays(7);

            _repository.User.UpdateUser(user);

            _repository.Save();

            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost, Route("register")]
        public IActionResult Register([FromBody] User user)
        {
            var candidate = _repository.User.GetUserByEmail(user.Email);
            if (candidate != null)
            {
                return StatusCode(409, new{ title= "Email already registered" });
            }
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid user object sent from client.");
                return BadRequest("Invalid model object");
            }

            _repository.User.CreateUser(user);
            _repository.Save();
            return NoContent();
        }
    }
}
