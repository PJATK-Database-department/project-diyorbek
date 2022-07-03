using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterUserDto registerUserDto);
    Task<string> LoginAsync(LoginUserDto loginUserDto);
}