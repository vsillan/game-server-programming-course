using System;
using System.Collections.Generic;

namespace game_server.Players
{
    public class Player
    {
        public Player()
        {
            Items = new List<Item>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreationDate { get; set; }
        public List<Item> Items { get; set; }
    }
}