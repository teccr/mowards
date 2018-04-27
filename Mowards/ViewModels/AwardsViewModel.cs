using System;
using System.Collections.ObjectModel;
using Mowards.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mowards.ViewModels
{
    public class AwardsViewModel : ViewModelBase
    {
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
        }

        protected override async void InitClass()
        {
            Categories = new ObservableCollection<AwardCategory>();
            await ExecuteSafeOperation(InitYearData);
        }

        protected override void InitCommands()
        {
            
        }

        #region Commands

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

        #endregion
    }
}
