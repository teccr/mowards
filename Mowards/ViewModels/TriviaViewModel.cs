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
            var levels = new int[] {
                1, 2, 3, 4, 5
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

        private ObservableCollection<TriviaChallengeResults> _challengeResults;
        public ObservableCollection<TriviaChallengeResults> ChallengeResults
        {
            get 
            {
                return _challengeResults;
            }
            set
            {
                _challengeResults = value;
                OnPropertyChanged("ChallengeResults");
            }
        }

        private int _selectedLevel;
        public int SelectedLevel
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

        private int[] _levels;
        public int[] Levels
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
