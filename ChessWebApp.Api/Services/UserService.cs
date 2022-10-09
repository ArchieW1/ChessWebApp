using ChessWebApp.Api.Domain;
using ChessWebApp.Api.Mapping;
using ChessWebApp.Api.Repositories;
using ChessWebApp.Shared.Contracts;
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
        UserDto? existingUser = await _userRepository.GetAsync(user.Id.Value);
        if (existingUser is not null)
        {
            string message = $"A user with id {user.Id} already exists";
            throw new ValidationException(message, new []
            {
                new ValidationFailure(nameof(User), message)
            });
        }

        UserDto userDto = user.ToCustomerDto();
        return await _userRepository.CreateAsync(userDto);
    }

    public async Task<User?> GetAsync(Guid id)
    {
        UserDto? userDto = await _userRepository.GetAsync(id);
        return userDto?.ToUser();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var userDtos = await _userRepository.GetAllAsync();
        return userDtos.Select(dto => dto.ToUser());
    }

    public async Task<bool> UpdateAsync(User user)
    {
        UserDto userDto = user.ToCustomerDto();
        return await _userRepository.UpdateAsync(userDto);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }
}
