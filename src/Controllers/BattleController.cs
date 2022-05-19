using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;

using Dapr.Client;

using toc_onboarding.Models;

namespace toc_onboarding.Controllers
{
    [ApiController]
    [Route("/")]
    public class BattleController : ControllerBase
    {
        private readonly ILogger<BattleController> _logger;

        const int MAX_PUNCHES = 20;

        public BattleController(ILogger<BattleController> logger)
        {
            _logger = logger;
        }

        [HttpPost("punch")]
        async public void Punch([FromBody] ClientPunch data)
        {
            // Get dpr client
            var client = new DaprClientBuilder().Build();

            // Get client info from state
            ChampionEnum champion = await client.GetStateAsync<ChampionEnum>("statestore", data.Id);

            if (champion != ChampionEnum.NoChampion)
            { 
                _logger.LogInformation($"POW!!! {champion} punches {data.Punches} times!!");
                PunchData pdata = new ()
                {
                    Champion = champion,
                    Punches = data.Punches > MAX_PUNCHES ? MAX_PUNCHES : data.Punches
                };

                CancellationTokenSource source = new ();
                CancellationToken cancellationToken = source.Token;
                await client.PublishEventAsync<PunchData>("pubsub", "battle", pdata, cancellationToken);
            }
            else
            {
                _logger.LogInformation($"No information for client {data.Id} exists. Ignoring...");
            }
        }
    }
}
