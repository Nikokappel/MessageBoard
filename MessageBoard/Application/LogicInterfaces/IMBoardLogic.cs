using Domain.Models;
using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IMBoardLogic
{
    Task<MessageBoard> CreateAsync(MBoardCreationDto dto);
    Task<IEnumerable<MessageBoard>> GetAsync(SearchMBoardParametersDto searchParameters);
    Task UpdateAsync(MBoardUpdateDto MessageBoard);
    Task DeleteAsync(int id);
    Task<MBoardBasicDto> GetByIdAsync(int id);
}