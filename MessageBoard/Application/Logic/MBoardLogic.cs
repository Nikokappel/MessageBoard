using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Models;
using Domain.DTOs;

namespace Application.Logic;

public class MBoardLogic : IMBoardLogic
{
    private readonly IMBoardDao mBoardDao;
    private readonly IUserDao userDao;

    public MBoardLogic(IMBoardDao mBoardDao, IUserDao userDao)
    {
        this.mBoardDao =mBoardDao;
        this.userDao = userDao;
    }
    
    public async Task<MessageBoard> CreateAsync(MBoardCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        
        MessageBoard messageBoard = new MessageBoard(user.UserId, dto.Title, dto.Body, GetTime(DateTime.Now));
        ValidateMessageBoard(messageBoard);
        MessageBoard created = await mBoardDao.CreateAsync(messageBoard);
        return created;
    }
    
    public Task<IEnumerable<MessageBoard>> GetAsync(SearchMBoardParametersDto searchParameters)
    {
        return mBoardDao.GetAsync(searchParameters);
    }
    
    public async Task UpdateAsync(MBoardUpdateDto dto)
    {
        MessageBoard? existing = await mBoardDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"MessageBoard with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        string bodyToUse = dto.Body ?? existing.Body;
        string timeToUse = GetTime(DateTime.Now);

        MessageBoard updated = new(userToUse.UserId, titleToUse, bodyToUse, timeToUse)
        {
            Id = existing.Id,
        };
        
        ValidateMessageBoard(updated);

        await mBoardDao.UpdateAsync(updated);
    }
    
    public async Task DeleteAsync(int id)
    {
        MessageBoard? existing = await mBoardDao.GetByIdAsync(id);

        if (existing == null)
        {
            throw new Exception($"MessageBoard with ID {id} not found!");
        }

        await mBoardDao.DeleteAsync(id);
    }
    
    public async Task<MBoardBasicDto> GetByIdAsync(int id)
    {
        MessageBoard? existing = await mBoardDao.GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"MessageBoard with id {id} not found");
        }

        return new MBoardBasicDto(existing.Id, existing.Owner.UserName, existing.Title, existing.Body);
    }
    
    private void ValidateMessageBoard(MessageBoard dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body cannot be empty.");
    }
    private static string GetTime(DateTime dateTime)
    {
        string time = dateTime.ToString("HH:mm:ss");

        return time;
    }
}