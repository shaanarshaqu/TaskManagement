using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Dependancies;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsers _users_list;
        private readonly IConfiguration _configuration;
        public UserController(IUsers users, IConfiguration configuration)
        {
            _users_list = users;
            _configuration = configuration;
        }

        [HttpGet(Name="getuser")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var data = _users_list.DisplayUsers();
            return Ok(data);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Post([FromBody]UserDto userdata)
        {       
            if ( userdata == null )
            {
                return BadRequest();
            }
            var data = _users_list.DisplayUsers();
            if (userdata.Id > 0)
            {
                return StatusCode(500);
            }
            userdata.Id = data.OrderByDescending(x => x.Id).FirstOrDefault().Id+1;
            _users_list.AddUser(userdata);
            return CreatedAtRoute("getuser",new{ id= userdata.Id}, userdata);
        }


        [HttpPost("validate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Validate([FromBody] LoginCredentials userdata)
        {
            if (userdata == null)
            {
                return BadRequest();
            }
            var existingUser = _users_list.ValidateUser(userdata);
            if(existingUser == null)
            {
                return BadRequest("invalid User Name or Password");
            }
            string token = GenerateJwtToken(existingUser);

            return Ok(new { Token = token });


        }

        private string GenerateJwtToken(UserDto user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            // Add additional claims as needed
        };

            var token = new JwtSecurityToken(
                //issuer: _configuration["Jwt:Issuer"],
                //audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

    }
}
