using ChatApp.Models;
using Microsoft.EntityFrameworkCore;

public class ChatContext : DbContext
{
    public DbSet<ChatUser> ChatUsers { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }

    public ChatContext(DbContextOptions<ChatContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatUser>().HasKey(u => u.Id);
        modelBuilder.Entity<ChatMessage>().HasKey(m => m.Id);
    }
}
