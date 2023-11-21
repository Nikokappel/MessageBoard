using Domain.Models;
using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IMBoardService
{
    Task CreateAsync(MBoardCreationDto dto);

    Task<ICollection<MessageBoard>> GetAsync(
        string? username,
        string? titleContains,
        string? body
    );
    
    Task<MBoardBasicDto> GetByIdAsync(int id);
    
    Task UpdateAsync(MBoardUpdateDto dto);

    Task DeleteAsync(int? id);
    Task<IEnumerable<MessageBoard>?> AsyncGetMessages(String? usernameContains = null);
}