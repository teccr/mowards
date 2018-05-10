using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mowards.MowardsService;

namespace Mowards.Models
{
    public class TriviaAnswer
    {
        public string Id
        {
            get;
            set;
        }
        public string username { get; set; }
        public bool IsCorrect { get; set; }
        public Award Award { get; set; }
        public Award UserAnswer { get; set; }
        public DateTime DoneWhen { get; set; } 
        public int Level
        {
            get;
            set;
        }

        public static async Task<ObservableCollection<TriviaAnswer>> GetSavedQuestions(int level)
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Get<ObservableCollection<TriviaAnswer>>(
                Utils.TRIVIA_URL + "?level=" + level.ToString());
        }

        public static async Task<Award[]> GetNewQuestions(int level, int maxQuestions)
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Get<Award[]>(
                Utils.TRIVIA_URL + "?level=" + level.ToString() +
                "&maxQuestions=" + maxQuestions.ToString()
            );
        }
    }
}
