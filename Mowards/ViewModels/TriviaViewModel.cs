using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Mowards.Models;
using Xamarin.Forms;
using System.Linq;
using Mowards.Views;

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
            _triviaResults = new ObservableCollection<TriviaChallengeResults>();
            Trivias = new ObservableCollection<TriviaProxy>();
            LoadTriviaResults();
        }

        protected override void InitCommands()
        {
            LoadQuestionsCommand = new Command<int>(LoadQuestions);
            SubmitAnswerCommand = new Command<string>(SubmitAnswer);
            SelectAnswerCommand = new Command<string>(SelectAnswer);
        }

        #endregion

        #region Commands

        public ICommand SelectAnswerCommand
        {
            get;
            set;
        }

        private void SelectAnswer(string Id)
        {
            SelectedAward = Id;
        }

        public ICommand SubmitAnswerCommand
        {
            get;
            set;
        }

        public async void SubmitAnswer(string question)
        {
            Func<Task> submit = new Func<Task> (async () =>
            {
                if( question != null )
                {
                    var currentQuestion = 
                        Trivias.Where( trivia => trivia.Id == question ).First();
                    var userAnswer = currentQuestion.Options.Where( option => option.Id == SelectedAward ).FirstOrDefault();
                    currentQuestion.Question.UserAnswer = userAnswer;
                    var triviaResult = await TriviaAnswer.SubmitAnswer(currentQuestion.Question);
                    currentQuestion.Question = triviaResult;
                    await App.Current.MainPage.DisplayAlert("Test", "Some test message", "Done");
                }
            });
            await ExecuteSafeOperation(submit);
        }

        public ICommand LoadQuestionsCommand
        {
            get;
            set;
        }

        private async void LoadQuestions(int level)
        {
            Func<Task> questions = async () =>
            {
                CurrentLevel = level;
                var unsortedItems = await TriviaAnswer.GetSavedQuestions(level);
                var result = new ObservableCollection<TriviaProxy>();
                foreach(var item in unsortedItems)
                {
                    TriviaProxy proxy = new TriviaProxy();
                    proxy.Question = item;
                    proxy.Options = new ObservableCollection<Award>();
                    result.Add(proxy);
                }
                if(result.Count < Utils.TRIVIA_QUESTIONS_LIMIT)
                {
                    var missingTriviaItemsCount = Utils.TRIVIA_QUESTIONS_LIMIT - result.Count;
                    var newQuestions = await TriviaAnswer.GetNewQuestions(level, missingTriviaItemsCount);
                    var sortedAwards = from award in newQuestions
                                       orderby award.Category
                                       group award by new { award.Category, award.Year } into awardsGroup
                                       select new AwardsGroup<string, Award>(
                                       $"{awardsGroup.Key.Year}-{awardsGroup.Key.Category}", awardsGroup);
                    foreach(var questionGroup in sortedAwards)
                    {
                        TriviaAnswer newTrivia = new TriviaAnswer();
                        newTrivia.Award = questionGroup.Where(award => award.Won == 1).FirstOrDefault();
                        newTrivia.Level = CurrentLevel;
                        TriviaProxy proxy = new TriviaProxy();
                        proxy.Question = newTrivia;
                        proxy.Options = questionGroup;
                        result.Add(proxy);
                    }
                }
                Trivias = result;
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TriviaQuestionsPanel());
            };
            await ExecuteSafeOperation(questions);
        }

        public async void LoadTriviaResults()
        {
            Func<Task> triviaFunction = async () =>
            {
                TriviaResults = await TriviaChallengeResults.GetTriviaChallengesResults();
            };
            await ExecuteSafeOperation(triviaFunction);
        }

        #endregion

        #region Properties

        private string _selectedAward;
        public string SelectedAward
        {
            get
            {
                return _selectedAward;
            }
            set
            {
                _selectedAward = value;
                if(_selectedAward != null)
                {
                    IsSubmitEnable = true;
                }
                OnPropertyChanged("SelectedAward");
            }
        }

        private bool _isSubmitEnable = false;
        public bool IsSubmitEnable
        {
            get
            {
                return IsBusy ? false : _isSubmitEnable;
            }
            set
            {
                _isSubmitEnable = value;
                OnPropertyChanged("IsSubmitEnable");
            }
        }

        private ObservableCollection<TriviaChallengeResults> _triviaResults;
        public ObservableCollection<TriviaChallengeResults> TriviaResults
        {
            get
            {
                return _triviaResults;
            }
            set
            {
                _triviaResults = value;
                OnPropertyChanged("TriviaResults");
            }
        }

        private int _currentLevel;
        public int CurrentLevel
        {
            get
            {
                return _currentLevel;
            }
            set
            {
                _currentLevel = value;
                OnPropertyChanged("CurrentLevel");
            }
        }

        private ObservableCollection<TriviaProxy> _trivias;
        public ObservableCollection<TriviaProxy> Trivias
        {
            get
            {
                return _trivias;
            }
            set
            {
                _trivias = value;
                OnPropertyChanged("Trivias");
            }
        }

        #endregion
    }
}
