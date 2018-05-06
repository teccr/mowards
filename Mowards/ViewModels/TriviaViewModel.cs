using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mowards.Models;

namespace Mowards.ViewModels
{
    public class TriviaViewModel : ViewModelBase
    {
        #region Initialization
        public TriviaViewModel()
        {
            InitClass();
            InitCommands();
        }

        protected override void InitClass()
        {
            var levels = new Dictionary<string, int[]>()
            {
                { "Level 1", new int[] { 2018, 2017, 2016, 2015, 2014 }},
                { "Level 2",  new int[] { 2013, 2012, 2011, 2010, 2009, 2008, 2007, 2006 } },
                { "Level 3",  new int[] { 2005, 2004, 2003, 2002, 2001, 2000, 1999, 1998, 1997  } },
                { "Level 4",  new int[] { 1996, 1995, 1994, 1993, 1992, 1991, 
                        1990, 1989, 1988, 1987, 1986, 1985, 1984, 1983, 1982, 1981, 1980  } },
                { "Level 5",  new int[] { 1979, 1978, 1977, 1976, 1975, 1974,
                        1973, 1972, 1971, 1970, 1969, 1968, 1967, 1966, 1965, 1964, 1963,
                        1962, 1961, 1960, 1959, 1958 } }
            };
            Levels = levels;
        }

        protected override void InitCommands()
        {
            
        }
        #endregion

        #region Commands

        #endregion

        #region Properties

        private string _selectedLevel;
        public string SelectedLevel
        {
            get
            {
                return _selectedLevel;
            }
            set
            {
                _selectedLevel = value;
                OnPropertyChanged("SelectedLevel");
            }
        }

        private Dictionary<string, int[]> _levels;
        public Dictionary<string, int[]> Levels
        {
            get
            {
                return _levels;
            }
            set
            {
                _levels = value;
                OnPropertyChanged("Levels");
            }
        }

        private ObservableCollection<TriviaAnswer> _selectedAnswers;
        public ObservableCollection<TriviaAnswer> SelectedAnswers
        {
            get
            {
                return _selectedAnswers;
            }
            set
            {
                _selectedAnswers = value;
                OnPropertyChanged("SelectedAnswers");
            }
        }

        #endregion
    }
}
