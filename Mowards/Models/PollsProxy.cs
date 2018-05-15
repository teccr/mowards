using System;
using Mowards.ViewModels;

namespace Mowards.Models
{
    public class PollsProxy : MutableDataObject
    {
        private PoolDefinition _definition;
        public PoolDefinition Definition
        {
            get
            {
                return _definition;
            }
            set
            {
                _definition = value;
                OnPropertyChanged("Definition");
            }
        }

        private PoolAnswer _answer;
        public PoolAnswer Answer
        {
            get
            {
                return _answer;
            }
            set
            {
                _answer = value;
                OnPropertyChanged("Answer");
            }
        }

        private PoolsResults _results;
        public PoolsResults Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
                OnPropertyChanged("Results");
            }
        }
    }
}
