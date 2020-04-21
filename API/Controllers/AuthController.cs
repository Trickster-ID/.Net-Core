using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Model;
using API.Repository.Data;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        DynamicParameters parameters = new DynamicParameters();
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly EmpRepository _employeerepository;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration, EmpRepository employeerepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _employeerepository = employeerepository;
        }

        // /register
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> InsertUser([FromBody] RegisterVM model)
        {
            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _employeerepository.Create2(model);
                await _userManager.AddToRoleAsync(user, "Employee");
                
            }
            return Ok(new { Email = user.Email });
        }

        [Route("login")] // /login
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginVM model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            using (var connection = new SqlConnection(_configuration.GetConnectionString("MyConnection")))
            {
                var procName = "SP_GetRole";
                parameters.Add("@Email", user.Email);
                IEnumerable<LoginVM> data = connection.Query<LoginVM>(procName, parameters, commandType: CommandType.StoredProcedure);
                foreach (LoginVM users in data)
                {
                    model.Role = users.Role;
                }
            }
            //var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claim = new[] {
                    new Claim("Email", user.Email),
                    new Claim("Role", model.Role)
                };
                var signinKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

                int expiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

                var token = new JwtSecurityToken(
                  issuer: _configuration["Jwt:Site"],
                  audience: _configuration["Jwt:Site"],
                  claim,
                  expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                  signingCredentials: new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(
                  new
                  {
                      token = new JwtSecurityTokenHandler().WriteToken(token),
                      expiration = token.ValidTo
                  });
            }
            return Unauthorized();
        }

    }
}