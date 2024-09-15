using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private readonly ChatContext _context;

    public ChatHub(ChatContext context)
    {
        _context = context;
    }

    public async Task SendMessage(string user, string message)
    {
        var chatMessage = new ChatMessage
        {
            Sender = user,
            Message = message,
            SentAt = DateTime.Now
        };

        _context.ChatMessages.Add(chatMessage);
        await _context.SaveChangesAsync();

        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public async Task GetUsers()
    {
        var users = await _context.ChatUsers.Select(u => u.Username).ToListAsync();
        await Clients.Caller.SendAsync("ReceiveUsers", users);
    }

    public async Task JoinChat(string username)
    {
        Console.WriteLine($"User {username} is joining the chat");

        var existingUser = await _context.ChatUsers
            .FirstOrDefaultAsync(u => u.Username == username);

        if (existingUser == null)
        {
            var user = new ChatUser
            {
                Username = username,
                ConnectedAt = DateTime.Now
            };

            _context.ChatUsers.Add(user);
            await _context.SaveChangesAsync();
        }

        var users = await _context.ChatUsers.Select(u => u.Username).ToListAsync();
        await Clients.All.SendAsync("ReceiveUsers", users);
    }
}
