using System;
using System.Collections.ObjectModel;
using Mowards.ViewModels;

namespace Mowards.Models
{
    public class TriviaProxy : MutableDataObject
    {
        private TriviaAnswer _question;
        public TriviaAnswer Question
        {
            get
            {
                return _question;
            }
            set
            {
                _question = value;
                OnPropertyChanged("Question");
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

        private ObservableCollection<Award> _options;
        public ObservableCollection<Award> Options
        {
            get
            {
                return _options;
            }
            set
            {
                _options = value;
                OnPropertyChanged("Options");
            }
        }
    }
}
