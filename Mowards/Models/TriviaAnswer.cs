using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mowards.MowardsService;
using Mowards.ViewModels;

namespace Mowards.Models
{
    public class TriviaAnswer : MutableDataObject
    {
        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string username { get; set; }

        public bool IsCorrect { get; set; }

        private Award _award;
        public Award Award 
        { 
            get
            {
                return _award;
            }
            set
            {
                _award = value;
                OnPropertyChanged("Award");
            }
        }

        private Award _userAnswer;
        public Award UserAnswer 
        { 
            get
            {
                return _userAnswer;
            }
            set
            {
                _userAnswer = value;
                OnPropertyChanged("UserAnswer");
            }
        }

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

        public static async Task<TriviaAnswer> SubmitAnswer(TriviaAnswer answer)
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Post<TriviaAnswer>(Utils.TRIVIA_URL, answer);
        }
    }
}
