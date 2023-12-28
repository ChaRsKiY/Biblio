using System.Collections.ObjectModel;
using BiblioServer.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using BiblioServer.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BiblioServer.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetAllUsers")]
    public async Task <ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        return Ok(await _context.Users.ToListAsync());
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationModel user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await _context.Users.AnyAsync(x => x.Email == user.Email))
        {
            return BadRequest("Пользователь с таким email уже существует");
        }

        string salt = BCrypt.Net.BCrypt.GenerateSalt(12);

        var newUser = new User
        {
            UserName = user.UserName,
            Email = user.Email,
            Salt = salt,
            HashedPassword = HashPassword(user.Password, salt),
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        return Ok("Регистрация успешна");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginModel loginModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == loginModel.Email);

        if (user == null || !VerifyPassword(loginModel.Password, user.HashedPassword))
        {
            return Unauthorized("Неверные учетные данные");
        }

        var token = GenerateJwtToken(user);

        return Ok(new { Token = token, Message = "Вход успешен" });
    }

    private string HashPassword(string password, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    private bool VerifyPassword(string EnteredPassword, string PasswordHash)
    {
        return BCrypt.Net.BCrypt.Verify(EnteredPassword, PasswordHash);
    }

    private string GenerateJwtToken(User user)
    {
        var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("awd5aw1d65wa1d56wa1d65w1f5wa61f51g6s51g651se65g1e651g5es156aw1dS56AD1F51561f65156F15F1561f651FW651651fW561F51156"));
        var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
        };

        var token = new JwtSecurityToken(
            issuer: "",
            audience: "",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: Credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}