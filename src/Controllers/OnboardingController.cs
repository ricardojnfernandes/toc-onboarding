using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapr.Client;
using toc_onboarding.Models;

namespace toc_onboarding.Controllers
{
    [ApiController]
    [Route("/")]
    public class OnboardingController : ControllerBase
    {
        private readonly ILogger<OnboardingController> _logger;

        public OnboardingController(ILogger<OnboardingController> logger)
        {
            _logger = logger;
        }

        [HttpGet("signup")]
        async public Task<SignupData> Signup()
        {
            // Get dpr client
            var client = new DaprClientBuilder().Build();

            // Compute client data
            Guid guid = Guid.NewGuid();
            Random rd = new Random();
            ChampionEnum champion = (ChampionEnum)rd.Next(1, 4);
            
            // Store it and retrieve
            await client.SaveStateAsync("statestore", guid.ToString(), champion);
            _logger.LogInformation($"New client id {guid.ToString()} got {champion}");

            SignupData data = new() { 
                Id = guid.ToString(),
                Champion = champion
            };
            return data;
        }
    }
}
