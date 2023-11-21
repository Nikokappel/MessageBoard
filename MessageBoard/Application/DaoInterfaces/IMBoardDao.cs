using Domain.Models;
using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface IMBoardDao
{
    Task<MessageBoard> CreateAsync(MessageBoard messageBoard);
    Task<IEnumerable<MessageBoard>> GetAsync(SearchMBoardParametersDto searchParameters);
    Task UpdateAsync(MessageBoard messageBoard);
    Task<MessageBoard> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}