using Microsoft.AspNetCore.Mvc;
using Anime_girl_rank.Data;
using Anime_girl_rank.Models;

namespace Anime_girl_rank.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CharacterController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, IFormFile? imageFile)
        {
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("", "โปรดใส่ชื่อตัวละคร");
                return View();
            }

            string imageUrl = "/images/default.png"; // Default image

            if (imageFile != null && imageFile.Length > 0)
            {
                // Ensure the wwwroot/images folder exists
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);
                
                // Create unique filename
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                imageUrl = "/images/" + uniqueFileName;
            }

            var character = new Character_girl
            {
                Name = name,
                ImageUrl = imageUrl,
                Score = 0,
                Wins = 0,
                TotalMatches = 0
            };

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "BackHome");
        }
    }
}
