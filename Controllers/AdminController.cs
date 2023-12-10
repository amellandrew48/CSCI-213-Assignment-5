using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicShop.Models;
using MusicShop.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyMusicShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly MusicShopContext _context;

        public AdminController(MusicShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var genres = _context.Genres.ToList();
            ViewBag.Genres = genres.Any() ? new SelectList(genres, "GenreId", "Name") : new SelectList(Enumerable.Empty<SelectListItem>());

            var artists = _context.Artists.ToList();
            ViewBag.Artists = artists.Any() ? new SelectList(artists, "ArtistId", "Name") : new SelectList(Enumerable.Empty<SelectListItem>());

            var musicList = _context.Musics
                        .Include(m => m.Artist)
                        .Include(m => m.Genre)
                        .Select(m => new MusicViewModel
                        {
                            Title = m.Title,
                            ArtistName = m.Artist.Name,
                            GenreName = m.Genre.Name
                        })
                        .ToList();

            var viewModel = new AdminIndexViewModel
            {
                MusicList = musicList
            };

            return View(viewModel);
        }



        public IActionResult MusicList()
        {
            var musicList = _context.Musics
                                    .Include(m => m.Artist)
                                    .Include(m => m.Genre)
                                    .Select(m => new MusicViewModel
                                    {
                                        Title = m.Title,
                                        ArtistName = m.Artist.Name,
                                        GenreName = m.Genre.Name,                                        
                                    })
                                    .ToList();

            return View(musicList);
        }

        [HttpPost]
        public IActionResult AddMusic(Music music)
        {
            if (ModelState.IsValid)
            {
                int? adminUserId = HttpContext.Session.GetInt32("AdminUserId");
                if (adminUserId.HasValue)
                {
                    music.UserId = adminUserId.Value;
                }

                _context.Musics.Add(music);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index", music);
        }


        [HttpPost]        
        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index", user);
        }

        [HttpPost]
        public IActionResult AddGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult AddArtist(Artist artist)
        {

            _context.Artists.Add(artist);
            _context.SaveChanges();
            return RedirectToAction("Index");
        
            
        }
    }
}
