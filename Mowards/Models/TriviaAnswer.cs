using System;
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

    }
}
