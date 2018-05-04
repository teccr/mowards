using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Mowards.Models;
using Mowards.Views;
using Xamarin.Forms;

namespace Mowards.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        #region Initialization

        public MainMenuViewModel()
        {
            InitClass();
            InitCommands();
            InitMenu();
        }

        protected override void InitClass()
        {

        }

        protected override void InitCommands()
        {
            MenuItemClickedCommand = new Command<int>(MenuItemClicked);
        }

        #endregion

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
        private MowardsUser _CurrentUser = new MowardsUser();
        public MowardsUser CurrentUser
        {
            get
            {
                return _CurrentUser;
            }

            set
            {
                _CurrentUser = value;
                OnPropertyChanged("CurrentUser");

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

        private void MenuItemClicked(int menuID)
        {
            if (menuID == 1)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MasterDetailContent());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            else if (menuID == 2)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new CategoriesFilterView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;

            }
            else if (menuID == 3)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MyFavoritesPage());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            else if (menuID == 4)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AwardsPollsView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            else if (menuID == 5)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MyReviewsView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            else if (menuID == 6)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AcademyAwardsTriviaView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            else if (menuID == 7)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TakePictureOscarView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            else if (menuID == 8)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new EditProfileView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            else if (menuID == 9)
            {
                ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new SignOutView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
        }

        public ICommand MenuItemClickedCommand
        {
            get;
            set;
        }
        #endregion
    }
}