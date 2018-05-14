using System;
using System.Collections.ObjectModel;
using Mowards.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Mowards.Views;

namespace Mowards.ViewModels
{
    public class AwardsViewModel : ViewModelBase
    {
        #region Initialization

        public AwardsViewModel()
        {
            InitClass();
            InitCommands();
        }

        private async Task InitYearData()
        {
            IsBusy = true;
            Years = await HttpClient.Get<ObservableCollection<int>>(Utils.YEARS_URL);
            MinYear = 0;
            MaxYear = Years.Max();
            MinYear = Years.Min();
            SelectedYear = MaxYear;
            await LoadCategories();
        }

        protected override async void InitClass()
        {
            
            Categories = new ObservableCollection<AwardCategory>();
            Awards = new ObservableCollection<AwardsGroup<string, Award>>();
            await ExecuteSafeOperation(InitYearData);
        }

        protected override void InitCommands()
        {

            
            GetAwardsByFiltersCommand = new Command(GetAwardsByFilters);
            ReduceYearCommand = new Command(ReduceYear);
            AddYearCommand = new Command(AddYear);
            SeeMovieDetailsCommand=new Command<int>(SeeMovieDetails);
            GetMovieCreditsCommand = new Command(GetMovieCredits);
        }

        private async void GetMovieCredits()
        {
            CurrentMovie.Credits = await Movie.SetMovieCredits(CurrentMovie.TMDBMovieID);
            OnPropertyChanged("CurrentMovie");
        }

        private async void SeeMovieDetails(int TMDBMovieID)
        {
            CurrentMovie = await Movie.GetMovieByTMDBId(TMDBMovieID);
            await((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MovieDetailsView());
        }

        private void AddYear()
        {
            SelectedYear= SelectedYear+1;
        }

        private void ReduceYear()
        {
            SelectedYear = SelectedYear - 1;
        }

        #endregion

        #region Commands

        public ICommand GetAwardsByFiltersCommand
        {
            get;
            set;
        }
        public ICommand ReduceYearCommand
        {
            get;
            set;
        }
        public ICommand AddYearCommand
        {
            get;
            set;
        }
        public ICommand SeeMovieDetailsCommand
        {
            get;
            set;
        }
        public ICommand GetMovieCreditsCommand
        {
            get;
            set;
        }
        
        private async void GetAwardsByFilters()
        {
            string[] selectedCategories = (
                _categories.Where(category => category.Selected).Select(
                    category => category.Category)).ToArray();
            var unsortedAwards = await Award.GetAwardsByFilters(
                SelectedYear, selectedCategories);
            var sortedAwards = from award in unsortedAwards
                               orderby award.Category
                               group award by award.Category into awardsGroup
                               select new AwardsGroup<string, Award>(
                                                awardsGroup.Key, awardsGroup);
            var oldAwards = new ObservableCollection<AwardsGroup<string, Award>>();
            foreach (var item in sortedAwards)
                oldAwards.Add(item);
            Awards = oldAwards;
            await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new FilteredAwardsView());

            

        }

        // It was not possible to bind the Slider value changed to a command, 
        // so we are using this function in combination with an event.
        public async Task LoadCategories()
        {
            Func<Task> operation = async () =>
            {
                Categories = await AwardCategory.GetAwardsCategoriesByYear(SelectedYear);
            };
            await ExecuteSafeOperation(operation);
        }
       
        #endregion

        #region Data properties

        private ObservableCollection<AwardCategory> _categories;
        public ObservableCollection<AwardCategory> Categories
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

        private ObservableCollection<int> _years;
        public ObservableCollection<int> Years
        {
            get 
            {
                return _years;
            }
            set 
            {
                _years = value;
                OnPropertyChanged("Years");
            }
        }
        private Movie _CurrentMovie { get; set; }
        public Movie CurrentMovie {
            get { return _CurrentMovie; }
            set { _CurrentMovie = value; OnPropertyChanged("CurrentMovie"); }
        }
        private int _SelectedYear = 2000;
        public int SelectedYear 
        {
            get 
            {
                return _SelectedYear;
            }
            set 
            {
                _SelectedYear = value;
                OnPropertyChanged("SelectedYear");
            }
        }

        private int _minYear = 1990;
        public int MinYear
        {
            get 
            {
                return _minYear;
            }
            set
            {
                _minYear = value;
                OnPropertyChanged("MinYear");
            }
        }

        private int _maxYear = 2000;
        public int MaxYear
        {
            get
            {
                return _maxYear;
            }
            set
            {
                _maxYear = value;
                OnPropertyChanged("MaxYear");
            }
        }
       

        private ObservableCollection<AwardsGroup<string, Award>> _awards;
        public ObservableCollection<AwardsGroup<string, Award>> Awards
        {
            get
            {
                return _awards;
            }
            set
            {
                _awards = value;
                OnPropertyChanged("Awards");
            }
        }

        #endregion
    }
}
