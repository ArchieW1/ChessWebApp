using ChessWebApp.Api.Contracts.Data;
using ChessWebApp.Api.Domain;
using ChessWebApp.Api.Mapping;
using ChessWebApp.Api.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace ChessWebApp.Api.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> CreateAsync(User user)
    {
        UserDto? existingUser = await _userRepository.GetAsync(user.Username.Value);
        if (existingUser is not null)
        {
            string message = $"A user with name {user.Username} already exists";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(User), message)
            });
        }

        UserDto userDto = user.ToUserDto();
        return await _userRepository.CreateAsync(userDto);
    }

    public async Task<User?> GetAsync(string username)
    {
        UserDto? userDto = await _userRepository.GetAsync(username);
        return userDto?.ToUser();
    }
    
    public async Task<User?> GetLoginAsync(string username)
    {
        UserDto? userLoginDto = await _userRepository.GetLoginAsync(username);
        return userLoginDto?.ToUser();
    }
    
    public async Task<User?> GetStatsAsync(string username)
    {
        UserDto? userStatsDto = await _userRepository.GetStatsAsync(username);
        return userStatsDto?.ToUser();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        IEnumerable<UserDto> userDtos = await _userRepository.GetAllAsync();
        return userDtos.Select(dto => dto.ToUser());
    }
    
    public async Task<IEnumerable<User>> GetAllStatsAsync()
    {
        IEnumerable<UserDto> userStatsDtos = await _userRepository.GetAllStatsAsync();
        return userStatsDtos.Select(dto => dto.ToUser());
    }
    
    public async Task<IEnumerable<User>> GetAllLoginAsync()
    {
        IEnumerable<UserDto> userLoginDtos = await _userRepository.GetAllLoginAsync();
        return userLoginDtos.Select(dto => dto.ToUser());
    }

    public async Task<bool> UpdateAsync(User user)
    {
        UserDto userDto = user.ToUserDto();
        return await _userRepository.UpdateAsync(userDto);
    }
    
    public async Task<bool> UpdateLoginAsync(User user)
    {
        UserDto userLoginDto = user.ToUserDto();
        return await _userRepository.UpdateLoginAsync(userLoginDto);
    }
    
    public async Task<bool> UpdateStatsAsync(User user)
    {
        UserDto userStatsDto = user.ToUserDto();
        return await _userRepository.UpdateStatsAsync(userStatsDto);
    }

    public async Task<bool> DeleteAsync(string username)
    {
        return await _userRepository.DeleteAsync(username);
    }
}
