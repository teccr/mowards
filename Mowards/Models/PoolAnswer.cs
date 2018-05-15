using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mowards.MowardsService;

namespace Mowards.Models
{
    public class PoolAnswer
    {
        public string Id
        {
            get;
            set;
        }

        public PoolDefinition Definition { get; set; }

        public PoolOption SelectedOption { get; set; }

        public string Username { get; set; }

        public static async Task<ObservableCollection<PoolAnswer>> GetAvailablePools()
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Get<ObservableCollection<PoolAnswer>>(Utils.POLLSANSWERS_URL);
        }

        public static async Task<PoolAnswer> SubmitAnswer(PoolAnswer userAnswer)
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Post<PoolAnswer>(Utils.POLLSANSWERS_URL, userAnswer);
        }
    }
}
