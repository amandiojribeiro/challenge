using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Services.CommentService;
using Domain.Model.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private IConfiguration configuration;
        private ILogger<AuthController> logger;

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        [HttpPost("CreateToken/{email}/{password}")]
        public async Task<IActionResult> CreateToken(string email, string password)
        {
            ///Important this code is just for example purposes.
            ///in real life we use something has an identity server to manage users and roles
            string[] userRoles = null;

            switch (password)
            {
                case "123":
                    userRoles = new string[] { "1" }; 
                    break;
                case "456":
                    userRoles = new string[] { "2" };
                    break;
                case "789":
                    userRoles = new string[] { "3" }; 
                    break;

            }

            try
            {
                if (email == "admin@admin.com")
                {
                    var fakeClaims = new List<Claim>();
                    fakeClaims.Add(new Claim("MembershipId", "111"));
                    var userIdentity = new ClaimsIdentity(fakeClaims);
                    userRoles.ToList().ForEach((role) => userIdentity.AddClaim(new Claim(ClaimTypes.Role, role)));

                    var userClaims = fakeClaims;

                    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YouCannotAlterTokenIfYouCannotHoldThisVeryLongKey"));
                    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


                    var jwtSecurityToken = new JwtSecurityToken(
                     issuer: "Fiver.Security.Bearer",
                     audience: "Fiver.Security.Bearer",
                     claims: userClaims,
                     expires: DateTime.UtcNow.AddMinutes(60),
                     signingCredentials: signingCredentials);

                    return await Task.FromResult<IActionResult>(Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        expiration = jwtSecurityToken.ValidTo
                    }));
                }

                return await Task.FromResult<IActionResult>(Unauthorized());
            }
            catch (Exception ex)
            {
                logger.LogError($"error while creating token: {ex}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "error while creating token");
            }
        }
    }
}