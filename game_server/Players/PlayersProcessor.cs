using System;
using System.Threading.Tasks;
using game_server.Repositories;

namespace game_server.Players
{
     public class PlayersProcessor
    {
        private IRepository _repository;
        
        public PlayersProcessor(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<Player> CreatePlayer(NewPlayer newPlayer)
        {
            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Name = newPlayer.Name
            };
            await _repository.CreatePlayer(player);
            return player;
        }
    }
}