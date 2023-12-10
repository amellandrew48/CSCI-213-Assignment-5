using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using MyMusicShop.Models;
using MusicShop.Data;
using System.Linq;
using System.Collections.Generic;

public class ClientController : Controller
{
    private readonly MusicShopContext _context;

    public ClientController(MusicShopContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("ClientUserId");
        if (!userId.HasValue)
        {
            // Redirect to login or handle accordingly
            return RedirectToAction("Index", "Home");
        }

        var userMusicLibrary = _context.Users
                                       .Where(u => u.UserId == userId.Value)
                                       .SelectMany(u => u.MusicLibrary)
                                       .ToList();

        var allMusic = _context.Musics
                               .Include(m => m.Artist)
                               .Include(m => m.Genre)
                               .ToList();

        var cartItems = _context.CartItems
                       .Where(c => c.UserId == userId.Value)
                       .ToList();

        var viewModel = new ClientIndexViewModel
        {
            UserMusicLibrary = userMusicLibrary,
            AllMusic = allMusic,
            CartItems = cartItems
        };

        return View(viewModel);
    }

    public IActionResult AddToCart(int musicId)
    {
        var userId = HttpContext.Session.GetInt32("ClientUserId");
        if (!userId.HasValue)
        {
            // Handle not logged in
            return RedirectToAction("Index", "Home");
        }

        var cartItem = new CartItem
        {
            UserId = userId.Value,
            MusicId = musicId,
            Quantity = 1 // or handle quantity logic
        };

        _context.CartItems.Add(cartItem);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult BuyMusic()
    {
        var userId = HttpContext.Session.GetInt32("ClientUserId");
        if (!userId.HasValue)
        {
            // Handle not logged in
            return RedirectToAction("Index", "Home");
        }

        var cartItems = _context.CartItems
                               .Where(c => c.UserId == userId.Value)
                               .ToList();

        foreach (var item in cartItems)
        {
            var user = _context.Users.Find(userId.Value);
            var music = _context.Musics.Find(item.MusicId);

            user.MusicLibrary.Add(music);
            _context.CartItems.Remove(item);
        }

        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
