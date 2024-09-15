using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly ChatContext _context;

    public HomeController(ChatContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Chat()
    {
        return View();
    }

    public async Task<IActionResult> Index()
    {
        var messages = await _context.ChatMessages
            .OrderByDescending(m => m.SentAt)
            .Take(50)
            .ToListAsync();

        return View(messages);
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
