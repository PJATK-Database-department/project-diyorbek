using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> RegisterAsync(RegisterUserDto registerUserDto)
    {
        var existingUser = await _context.Users.SingleOrDefaultAsync(user => user.Login == registerUserDto.Login);

        if (existingUser != null)
            throw new Exception();
        // return new RegisterResponseErrorDto {Message = "User exists"};

        var salt = PasswordHelper.GetSecureSalt();
        var passwordHash = PasswordHelper.HashUsingPbkdf2(registerUserDto.Password, salt);
        var user = new User
        {
            Login = registerUserDto.Login,
            PasswordHash = passwordHash,
            PasswordSalt = Convert.ToBase64String(salt)
        };

        await _context.Users.AddAsync(user);

        var saveResponse = await _context.SaveChangesAsync();

        if (saveResponse == 0) throw new Exception();

        return user.Login;
    }

    public async Task<string> LoginAsync(LoginUserDto loginUserDto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(user => user.Login == loginUserDto.Login);

        if (user == null)
            throw new Exception("User not found");

        var passwordHash =
            PasswordHelper.HashUsingPbkdf2(loginUserDto.Password, Convert.FromBase64String(user.PasswordSalt));

        if (user.PasswordHash != passwordHash)
            throw new Exception("Invalid Password");

        return user.Login;
    }
}