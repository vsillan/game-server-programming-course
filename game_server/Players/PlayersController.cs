using System;
using Microsoft.AspNetCore.Mvc;
using game_server.Validation;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace game_server.Players
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly PlayersProcessor _playerProcessor;

        public PlayersController(ILogger<PlayersController> logger, PlayersProcessor playersProcessor)
        {
            _logger = logger;
            _playerProcessor = playersProcessor;
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
        public Task<Player> Create(NewPlayer newPlayer)
        {
            _logger.LogInformation("Creating player with name " + newPlayer.Name);
            return _playerProcessor.CreatePlayer(newPlayer);
        }

        [HttpDelete]
        [Route("{playerId}")]
        public Task<Player> Ban(Guid playerId)
        {
            throw new NotImplementedException();
        }
    }
}