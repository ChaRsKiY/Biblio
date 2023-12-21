using System.Collections.ObjectModel;
using BiblioServer.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using BiblioServer.Context;

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
            Password = HashPassword(user.Password, salt),
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

        if (user == null || !VerifyPassword(loginModel.Password, user.Password))
        {
            return Unauthorized("Неверные учетные данные");
        }

        return Ok("Вход успешен");
    }

    private string HashPassword(string password, string salt)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    private bool VerifyPassword(string EnteredPassword, string PasswordHash)
    {
        return BCrypt.Net.BCrypt.Verify(EnteredPassword, PasswordHash);
    }

}