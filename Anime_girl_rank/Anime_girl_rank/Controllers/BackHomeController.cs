using Microsoft.AspNetCore.Mvc;
using Anime_girl_rank.Models; // ตรวจสอบชื่อ Namespace ของ Character_girl ให้ถูก

namespace Anime_girl_rank.Controllers // เปลี่ยนเป็น Controllers
{
    public class BackHomeController : Controller // ต้องสืบทอดจาก Controller
    {
        // ใช้ static เพื่อให้ข้อมูลไม่หายเวลา Refresh หน้า
        private static List<Character_girl> characters = new List<Character_girl> {
            new Character_girl { Id = 1, Name = "Boa Hancock", ImageUrl = "/images/boa.png", Wins = 0 },
            new Character_girl { Id = 2, Name = "Lacus Clyne", ImageUrl = "/images/lacus.png", Wins = 0 },
            new Character_girl { Id = 3, Name = "Saeko Busujima", ImageUrl = "/images/senpai.jpg", Wins = 0 }
        };

        [HttpGet]
        public IActionResult Index()
        {
            if (characters.Count < 2) return Content("ต้องมีตัวละครอย่างน้อย 2 ตัว");

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
            // ใช้คำสั่งนี้เพื่อป้องกันการพังถ้าหา ID ไม่เจอ
            var winner = characters.FirstOrDefault(c => c.Id == winnerId);

            if (winner != null)
            {
                winner.Wins++;
            }

            // ลองเปลี่ยนมาใช้ท่านี้เพื่อความชัวร์
            return RedirectToAction(nameof(Index));
        }
    }
}