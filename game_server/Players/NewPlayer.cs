using System.ComponentModel.DataAnnotations;

namespace game_server.Players
{
    public class NewPlayer
    {
        [Required]
        public string Name { get; set; }
    }
}