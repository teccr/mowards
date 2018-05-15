using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mowards.MowardsService;

namespace Mowards.Models
{
    public class PoolsResults
    {
        public string Category
        {
            get;
            set;
        }

        public string Option
        {
            get;
            set;
        }

        public int Year
        {
            get;
            set;
        }

        public int Votes
        {
            get;
            set;
        }

        public static async Task<ObservableCollection<PoolsResults>> GetAvailablePools()
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Get<ObservableCollection<PoolsResults>>(Utils.POLLSRESULTS_URL);
        }
    }
}
