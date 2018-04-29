using System;
using System.Collections.ObjectModel;
using Mowards.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Mowards.Models;
using Mowards.Views;

namespace Mowards.ViewModels
{
    public class AwardsViewModel : ViewModelBase
    {
        public AwardsViewModel()
        {
            InitClass();
            InitCommands();
            InitMenu();
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
            MenuItemClickedCommand = new Command<int>(MenuItemClicked);

        }
      
        #region Instances
        private ObservableCollection<Models.MenuItem> _InitialMenu = new ObservableCollection<Models.MenuItem>();
        public ObservableCollection<Models.MenuItem> InitialMenu
        {
            get
            {
                return _InitialMenu;
            }

            set
            {
                _InitialMenu = value;
                OnPropertyChanged("InitialMenu");

            }
        }
        private void InitMenu()
        {
            InitialMenu = new ObservableCollection<Models.MenuItem>() { new Models.MenuItem() {ID=1, Logo="Home_48px.png", Text="Main Page", Description=""},
                                                                 new Models.MenuItem() {ID=2, Logo="List_50px.png", Text="Academy Winners / Nominees", Description="" },
                                                                 new Models.MenuItem() {ID=3, Logo="StarFilled_48px.png", Text="My Favorites", Description="" },
                                                                 new Models.MenuItem() {ID=4, Logo="BarChart_52px.png", Text="Awards Polls", Description="" },
                                                                 new Models.MenuItem() {ID=5, Logo="Edit_48px.png", Text="My Reviews", Description="" },
                                                                 new Models.MenuItem() {ID=6, Logo="Rocket_48px.png", Text="Academy Awards Trivia", Description="" },
                                                                 new Models.MenuItem() {ID=7, Logo="Unsplash_48px.png", Text="Take a Picture Holding an Oscar", Description="" },
                                                                 new Models.MenuItem() {ID=8, Logo="Account_50px.png", Text="Edit Profile", Description="" },
                                                                 new Models.MenuItem() {ID=9, Logo="SignOut_50px.png", Text="Sign Out", Description="" }
                                                                  };

        }
        #endregion
        #region Commands
        public ICommand MenuItemClickedCommand
        {
            get;
            set;
        }


        public async Task LoadCategories()
        {
            Func<Task> operation = async () =>
            {
                Categories = await AwardCategory.GetAwardsCategoriesByYear(SelectedYear);
            };
            await ExecuteSafeOperation(operation);
        }

        #endregion
        private void MenuItemClicked(int menuID)
        {
            if (menuID == 1)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MasterDetailContent());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 2)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new CategoriesFilterView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
                
            }
            if (menuID == 3)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MyFavoritesPage());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 4)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AwardsPollsView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 5)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MyReviewsView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 6)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AcademyAwardsTriviaView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 7)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TakePictureOscarView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 8)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new EditProfileView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 9)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new SignOutView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
        }
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
