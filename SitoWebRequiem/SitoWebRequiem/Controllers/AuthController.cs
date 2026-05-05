using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SitoWebRequiem.SharedModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SitoWebRequiem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly WebDbContext context;
        private readonly IConfiguration configuration;
        public AuthController(WebDbContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpDto dto)
        {
            if (await context.Users.AnyAsync(u => u.Username == dto.Username))
            {
                return BadRequest("Username già scelto");
            }else if (dto.Username.Contains(" "))
            {
                return BadRequest("L'username non può contenere spazi");
            }

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Ok("Utente creato con successo");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized("Credenziali errate");
            }
            var token = GenerateJwt(user);
            return Ok(new {token});
        }

        [Authorize]
        [HttpPost("delete-account")]
        public async Task<IActionResult> EliminaAccount()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("Utente invalido");
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return Ok("Account eliminato con successo");
        }


        private string GenerateJwt(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
