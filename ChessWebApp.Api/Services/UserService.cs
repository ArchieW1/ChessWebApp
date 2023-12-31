﻿using ChessWebApp.Api.Contracts.Data;
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

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var userDtos = await _userRepository.GetAllAsync();
        return userDtos.Select(dto => dto.ToUser());
    }

    public async Task<bool> UpdateAsync(User user)
    {
        UserDto userDto = user.ToUserDto();
        return await _userRepository.UpdateAsync(userDto);
    }

    public async Task<bool> DeleteAsync(string username)
    {
        return await _userRepository.DeleteAsync(username);
    }
}
