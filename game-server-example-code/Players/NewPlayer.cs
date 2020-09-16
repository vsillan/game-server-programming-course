using System.ComponentModel.DataAnnotations;

namespace game_server.Players
{
    public class NewPlayer
    {
        [StringLength(5)]
        public string Name { get; set; }
    }
}