using Application.DaoInterfaces;
using Domain.Models;
using Domain.DTOs;

namespace FileData.DAOs;

public class MBoardFileDao : IMBoardDao
{
    private readonly FileContext context;

    public MBoardFileDao(FileContext context)
    {
        this.context = context;
    }
    public Task<MessageBoard> CreateAsync(MessageBoard messageBoard)
    {
        int messageBoardId = 1;
        if (context.MessageBoards.Any())
        {
            messageBoardId = context.MessageBoards.Max(t => t.Id);
            messageBoardId++;
        }

        messageBoard.Id = messageBoardId;
        
        context.MessageBoards.Add(messageBoard);
        context.SaveChanges();

        return Task.FromResult(messageBoard);
    }
    
     public Task<IEnumerable<MessageBoard>> GetAsync(SearchMBoardParametersDto searchParameters)
    {
        IEnumerable<MessageBoard> messageBoards = context.MessageBoards.AsEnumerable();
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            messageBoards = context.MessageBoards.Where(t =>
                t.Owner.UserName.Equals(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
        }
        if (searchParameters.UserId != null)
        {
            messageBoards = messageBoards.Where(t =>
                t.Owner.UserId == searchParameters.UserId);
        }
        if (searchParameters.BodyContains != null)
        {
            messageBoards = messageBoards.Where(t =>
                t.Body.Contains(searchParameters.BodyContains,StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            messageBoards = messageBoards.Where(t =>
                t.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(messageBoards);
    }

    public Task UpdateAsync(MessageBoard toUpdate)
    {
        MessageBoard? existing = context.MessageBoards.FirstOrDefault(t => t.Id == toUpdate.Id);
        if (existing == null)
        {
            throw new Exception($"MessageBoard with id {toUpdate.Id} does not exist!");
        }

        context.MessageBoards.Remove(existing);
        context.MessageBoards.Add(toUpdate);
        
        context.SaveChanges();
        
        return Task.CompletedTask;
    }

    public Task<MessageBoard?> GetByIdAsync(int id)
    {
        MessageBoard? existing = context.MessageBoards.FirstOrDefault(t =>
            t.Id == id);
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        MessageBoard? existing = context.MessageBoards.FirstOrDefault(t => t.Id == id);
        if (existing == null)
        {
            throw new Exception($"MessageBoards with id {id} does not exist!");
        }

        context.MessageBoards.Remove(existing);
        context.SaveChanges();
        
        return Task.CompletedTask;
    }
    
}