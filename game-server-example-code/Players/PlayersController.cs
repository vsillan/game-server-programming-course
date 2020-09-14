using System;
using Microsoft.AspNetCore.Mvc;
using game_server.Validation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using game_server.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace game_server.Players
{
    [ApiController]
    [Route("api/players")]
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
        public async Task<Player[]> GetAll(int minLevel)
        {
            return new Player[] { new Player() { Level = minLevel }, new Player() { Level = (minLevel + 1) } };
        }

        [HttpGet]
        [Route("{playerId}")]
        public Task<Player> Get(string playerId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("level-counts")]
        public Task<LevelCount[]> Get()
        {
            return _repository.GetLevelCounts();
        }

        [HttpPost]
        [Route("")]
        [ValidateModel]
        public async Task<Player> Create([FromBody] NewPlayer newPlayer)
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
    }
}