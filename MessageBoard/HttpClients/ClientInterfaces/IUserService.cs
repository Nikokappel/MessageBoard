using Domain.Models;
using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDto dto);

    Task<IEnumerable<User>> AsyncGetUser(String? usernameContains = null);
}