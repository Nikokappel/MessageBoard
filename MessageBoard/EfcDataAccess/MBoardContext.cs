using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class MBoardContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<MessageBoard> MessageBoards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/MessageBoard.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);            
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MessageBoard>().HasKey(messageBoard => messageBoard.Id);
        modelBuilder.Entity<User>().HasKey(user => user.UserId);
    }
    
   
}