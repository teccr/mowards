using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mowards.MowardsService;

namespace Mowards.Models
{
    public class PoolDefinition
    {
        public string Id
        {
            get;
            set;
        }
        public int Year { get; set; }
        public string Category { get; set; }
        public bool Closed { get; set; }
        public bool Visible { get; set; }

        public List<PoolOption> Options { get; set; }

        public static async Task<ObservableCollection<PoolDefinition>> GetAvailablePools()
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Get<ObservableCollection<PoolDefinition>>(Utils.POLLS_URL);
        }
    }
}
