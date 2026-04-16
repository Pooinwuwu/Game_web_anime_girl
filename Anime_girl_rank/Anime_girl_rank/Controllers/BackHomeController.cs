using Microsoft.AspNetCore.Mvc;
using Anime_girl_rank.Models;
using Anime_girl_rank.Data;

namespace Anime_girl_rank.Controllers
{
    public class BackHomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BackHomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var characters = _context.Characters.ToList();

            if (characters.Count < 2) 
            {
                // ถ้าไม่มีตัวละครในฐานข้อมูลเลย เราจะสร้างตัวละครจำลองเริ่มต้นให้
                if (characters.Count == 0)
                {
                    _context.Characters.AddRange(new List<Character_girl>
                    {
                        new Character_girl { Name = "Boa Hancock", ImageUrl = "/images/boa.png", Wins = 0 },
                        new Character_girl { Name = "Lacus Clyne", ImageUrl = "/images/lacus.png", Wins = 0 },
                        new Character_girl { Name = "Saeko Busujima", ImageUrl = "/images/senpai.jpg", Wins = 0 }
                    });
                    _context.SaveChanges();
                    characters = _context.Characters.ToList();
                }

                if (characters.Count < 2) 
                    return Content("ต้องมีตัวละครอย่างน้อย 2 ตัว โปรดเพิ่มตัวละครใหม่");
            }

            var random = new Random();
            var char1 = characters[random.Next(characters.Count)];

            // สุ่มตัวที่ 2 จนกว่าจะไม่ซ้ำกับตัวแรก
            Character_girl char2;
            do
            {
                char2 = characters[random.Next(characters.Count)];
            } while (char2.Id == char1.Id);

            ViewBag.Char1 = char1;
            ViewBag.Char2 = char2;

            return View();
        }

        [HttpPost]
        public IActionResult Vote(int winnerId)
        {
            var winner = _context.Characters.FirstOrDefault(c => c.Id == winnerId);

            if (winner != null)
            {
                winner.Wins++;
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}