using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace toc_onboarding.Models
{
    public class SignupData
    {
        public ChampionEnum Champion { get; set; }

        public string Id { get; set; }
    }
}
