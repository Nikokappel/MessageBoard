﻿using Domain.Models;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<User> CreateAsync(UserCreationDto userToCreate);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters);
}