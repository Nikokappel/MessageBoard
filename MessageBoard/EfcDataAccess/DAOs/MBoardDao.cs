using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class MBoardDao :IMBoardDao
{
    private readonly MBoardContext context;

    public MBoardDao(MBoardContext context)
    {
        this.context = context;
    }
    
   
    public async Task<MessageBoard> CreateAsync(MessageBoard messageBoard)
    {
        EntityEntry<MessageBoard> added = await context.MessageBoards.AddAsync(messageBoard);
        await context.SaveChangesAsync();
        return added.Entity;
    }

   
    public async Task<IEnumerable<MessageBoard>> GetAsync(SearchMBoardParametersDto searchParams)
    {
        IQueryable<MessageBoard> query = context.MessageBoards.Include(messageBoard => messageBoard.Owner).AsQueryable();
    
        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(messageBoard =>
                messageBoard.Owner.UserName.ToLower().Equals(searchParams.Username.ToLower()));
        }
    
        if (searchParams.UserId != null)
        {
            query = query.Where(t => t.Owner.UserId == searchParams.UserId);
        }
    
        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            query = query.Where(t =>
                t.Title.ToLower().Contains(searchParams.TitleContains.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(searchParams.BodyContains))
        {
            query = query.Where(t =>
                t.Body.ToLower().Contains(searchParams.BodyContains.ToLower()));
        }

        List<MessageBoard> result = await query.ToListAsync();
        return result;
    }
    
    
    public async Task UpdateAsync(MessageBoard messageBoard)
    {
        context.MessageBoards.Update(messageBoard);
        await context.SaveChangesAsync();
    }
    
    public async Task<MessageBoard?> GetByIdAsync(int mBoardId)
    {
        MessageBoard? found = await context.MessageBoards
            .Include(messageBoard => messageBoard.Owner)
            .SingleOrDefaultAsync(messageBoard => messageBoard.Id == mBoardId);
        return found;
    }

    public async Task DeleteAsync(int id)
    {
        MessageBoard? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"MessageBoard with id {id} not found");
        }

        context.MessageBoards.Remove(existing);
        await context.SaveChangesAsync();
    }
    
}