namespace Anime_girl_rank.Models
{
    public class Character_girl
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Score { get; set; }
        public int Wins { get; set; }        // จำนวนครั้งที่ถูกเลือก
        public int TotalMatches { get; set; } // จำนวนครั้งที่ปรากฏตัว
    }
}
