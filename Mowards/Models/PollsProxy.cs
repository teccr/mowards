using System;
using System.Collections.ObjectModel;
using Microcharts;
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

        private ObservableCollection<Entry> _results;
        public ObservableCollection<Entry> Results
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

        private Chart _chart;
        public Chart CurrentChart
        {
            get
            {
                return _chart;
            }
            set
            {
                _chart = value;
                OnPropertyChanged("CurrentChart");
            }
        }
    }
}
