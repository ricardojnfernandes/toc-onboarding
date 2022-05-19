using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace toc_onboarding.Models
{
    public class ClientPunch
    {
        public string Id { get; set; }

        public int Punches { get; set; }
    }
}
