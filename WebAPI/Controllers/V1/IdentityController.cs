using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.SwaggerExamples.Responses;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly IConfiguration _configuration;

        public IdentityController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManger = roleManger;
            _configuration = configuration;
        }

        /// <summary>
        /// Registers the user in the system
        /// </summary>
        /// <response code="200">User created successfully!</response>
        /// <response code="500">User already exists!</response>
        [ProducesResponseType(typeof(RegisterResponseStatus200), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegisterResponseStatus500), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel register)
        {
            var userExists = await _userManager.FindByNameAsync(register.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Succeeded = false,
                    Message = "User already exists!"
                });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Username
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                {
                    Succeeded = false,
                    Message = "User creation failed! Please check user details and try agian.",
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            if (!await _roleManger.RoleExistsAsync(UserRoles.User))
                await _roleManger.CreateAsync(new IdentityRole(UserRoles.User));

            await _userManager.AddToRoleAsync(user, UserRoles.User);

            return Ok(new Response { Succeeded = true, Message = "User created successfully!" });
        }

        /// <summary>
        /// Registers the admin in the system
        /// </summary>
        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdminAsync(RegisterModel register)
        {
            var userExists = await _userManager.FindByNameAsync(register.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Succeeded = false,
                    Message = "User already exists!"
                });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Username
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                {
                    Succeeded = false,
                    Message = "User creation failed! Please check user details and try agian.",
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            if (!await _roleManger.RoleExistsAsync(UserRoles.Admin))
                await _roleManger.CreateAsync(new IdentityRole(UserRoles.Admin));

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            return Ok(new Response { Succeeded = true, Message = "User created successfully!" });
        }

        /// <summary>
        /// Logs the user into the system
        /// </summary>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new AuthSuccessResponse()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }
    }
}
