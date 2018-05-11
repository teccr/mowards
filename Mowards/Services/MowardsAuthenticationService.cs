using System;
using System.Threading.Tasks;
using Mowards.MowardsService;

namespace Mowards.Services
{
    public class MowardsAuthenticationService
    {
        public async Task<bool> AcquireTokenAsync()
        {
            MowardsHttp httpClient = new MowardsHttp();

            return false;
        }
    }
}
