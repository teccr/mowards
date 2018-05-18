using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mowards.Models;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Microcharts;

namespace Mowards.ViewModels
{
    public class PollViewModel : ViewModelBase
    {
        #region Initalization

        public PollViewModel()
        {
            InitClass();
            InitCommands();
        }

        protected override void InitClass()
        {
            Polls = new ObservableCollection<PollsProxy>();
            GetAvailablePools();
            GetPoolResults();
            GetUserAnswers();
        }

        protected override void InitCommands()
        {
            SubmitAnswerCommand = new Command<string>(SubmitAnswer);
        }

        #endregion

        #region Common Functions

        private Random rnd = new Random();

        private async void GetAvailablePools()
        {
            Func<Task> pools = new Func<Task>(
                async () =>
                {
                    var availablePools = await PoolDefinition.GetAvailablePools();
                    var proxies = availablePools.Select( poll => new PollsProxy() { Definition = poll} );
                    Polls = new ObservableCollection<PollsProxy>(proxies);
                    Categories = new ObservableCollection<string> (Polls.Select(p => p.Definition.Category));
                }
            );

            await ExecuteSafeOperation( pools );
        }

        private async void GetPoolResults()
        {
            Func<Task> results = new Func<Task>(
                async () =>
                {
                    var query = await PoolsResults.GetAvailablePools();
                    if(query.Count() > 0)
                    {
                        foreach(PoolsResults poolResult in query)
                        {
                            var proxy = Polls.Where(p => p.Definition.Category == poolResult.Category).FirstOrDefault();
                            proxy.Results = new ObservableCollection<Microcharts.Entry>();
                            var entry = new Microcharts.Entry(poolResult.Votes);
                            entry.Label = poolResult.Option;
                            var newColor = new SkiaSharp.SKColor((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
                            entry.Color = newColor;
                            entry.ValueLabel = poolResult.Votes.ToString();
                            proxy.Results.Add(entry);
                        }
                        foreach( var poll in Polls )
                        {
                            if (poll.Answer != null)
                            {
                                poll.CurrentChart = new PieChart() 
                                { 
                                    Entries = poll.Results, 
                                    LabelTextSize=50
                                };
                            }
                        }
                    }
                }
            );

            await ExecuteSafeOperation(results);
        }

        private async void GetUserAnswers()
        {
            Func<Task> results = new Func<Task>(
                async () =>
                {
                    var query = await PoolAnswer.GetAvailablePools();
                    if(query.Count() > 0)
                    {
                        foreach(PoolAnswer answer in query)
                        {
                            var proxy = Polls.Where( p => p.Definition.Id == answer.Definition.Id ).FirstOrDefault();
                            proxy.Answer = answer;
                        }
                    }
                }
            );
            await ExecuteSafeOperation(results);
        }

        #endregion

        #region Commands

        public ICommand SubmitAnswerCommand
        {
            get;
            set;
        }

        public async void SubmitAnswer(string Id)
        {
            Func<Task> func = new Func<Task>(
                async () => {
                    PoolAnswer answer = new PoolAnswer();
                    var poll = Polls.Where(p => p.Definition.Id == Id).FirstOrDefault();
                    answer.Definition = poll.Definition;
                    answer.SelectedOption = SelectedAnswer;
                    answer = await PoolAnswer.SubmitAnswer(answer);
                    poll.Answer = answer;
                    GetPoolResults();
                }
            );
            await ExecuteSafeOperation( func );
        }

        #endregion

        #region Properties

        private ObservableCollection<PollsProxy> _polls;
        public ObservableCollection<PollsProxy> Polls
        {
            get
            {
                return _polls;
            }
            set
            {
                _polls = value;
                OnPropertyChanged("Polls");
            }
        }

        private ObservableCollection<string> _categories;
        public ObservableCollection<string> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        private int _position;
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                OnPropertyChanged("Position");
            }
        }

        private PoolOption _selectedAnswer;
        public PoolOption SelectedAnswer
        {
            get
            {
                return _selectedAnswer;
            }
            set
            {
                _selectedAnswer = value;
                OnPropertyChanged("SelectedAnswer");
            }
        }

        #endregion
    }
}
