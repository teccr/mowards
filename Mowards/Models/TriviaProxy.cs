using System;
using System.Collections.ObjectModel;
using Mowards.ViewModels;

namespace Mowards.Models
{
    public class TriviaProxy : MutableDataObject
    {
        public TriviaAnswer Question
        {
            get;
            set;
        }

        public bool ShowOptions
        {
            get
            {
                if(Question == null || Question.UserAnswer == null)
                {
                    return false;
                }
                return true;
            }
        }

        public bool ShowAnswer
        {
            get
            {
                return !ShowOptions;
            }
        }

        public string Id
        {
            get
            {
                if(Question != null)
                {
                    return Question.Award.Year.ToString() + Question.Award.Category;
                }
                return "";
            }
        }

        public ObservableCollection<Award> Options
        {
            get;
            set;
        }
    }
}
