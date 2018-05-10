using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mowards.MowardsService;

namespace Mowards.Models
{
    public class TriviaChallengeResults
    {
        public string Id
        {
            get;
            set;
        }

        public int Level 
        { 
            get; 
            set; 
        }

        public int CorrectAnswers 
        { 
            get; 
            set; 
        }

        public static async Task<ObservableCollection<TriviaChallengeResults>> GetTriviaChallengesResults()
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Get<ObservableCollection<TriviaChallengeResults>>(
                Utils.TRIVIA_URL);
        }


    }
}
