using System;
using Microsoft.AspNetCore.Mvc;
using game_server.Validation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using game_server.Repositories;

namespace game_server.Players
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IRepository _repository;

        public PlayersController(ILogger<PlayersController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        public Task<Player[]> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{playerId}")]
        public Task<Player> Get(string playerId)
        {

            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("")]
        [ValidateModel]
        public async Task<Player> Create(NewPlayer newPlayer)
        {
            _logger.LogInformation("Creating player with name " + newPlayer.Name);
            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Name = newPlayer.Name
            };
            await _repository.CreatePlayer(player);
            return player;
        }

        [HttpDelete]
        [Route("{playerId}")]
        public Task<Player> Ban(Guid playerId)
        {
            throw new NotImplementedException();
        }
    }
}