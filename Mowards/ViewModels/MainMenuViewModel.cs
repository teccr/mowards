using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Mowards.Models;
using Mowards.MowardsService;
using Mowards.Services;
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
            var userViewModel = ViewModelFactory.GetInstance<UserViewModel>();
            UserEmail= userViewModel.CurrentUser.Email;
            UpdateCurrentUserImage();
        }

        public async void UpdateCurrentUserImage() {
            try
            {
                var userViewModel = ViewModelFactory.GetInstance<UserViewModel>();
                await userViewModel.SetUserFullInformation(UserEmail);
                UserPicture = userViewModel.ImageLocation;
                if (UserPicture== "" || UserPicture==null) {
                    string img = "";
                    img = "User_104px.png";
                    UserPicture = img;
                    userViewModel.ImageLocation = img;
                }
                OnPropertyChanged("UserPicture");

            }
            catch (Exception ex)
            {
                string img ="";
                img = "User_104px.png";
                UserPicture = img;
                ViewModelFactory.GetInstance<UserViewModel>().ImageLocation = img;
            }
        }

        public async void UpdateCurrentUserImage(string img)
        {
            Func<Task> func = new Func<Task>(
                async () =>
                {
                    await LoginInfo.SaveProfileImage(img, UserEmail);
                    UserPicture = img;
                    OnPropertyChanged("UserPicture");
                }
            );

            await ExecuteSafeOperation(func);
        }

        protected override void InitCommands()
        {
            MenuItemClickedCommand = new Command<int>(MenuItemClicked);
            ToolbarItemCommand = new Command(ToolbarAboutView);
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

        private string _UserEmail { get; set; }
        public string UserEmail {
            get { return _UserEmail; } set { _UserEmail = value; OnPropertyChanged("UserEmail"); } }
        private string _UserPicture { get; set; }
        public string UserPicture
        {
            get { return _UserPicture; }
            set { _UserPicture = value; OnPropertyChanged("UserPicture"); }
        }
        private void InitMenu()
        {
            InitialMenu = new ObservableCollection<Models.MenuItem>() { new Models.MenuItem() {ID=1, Logo="Home_48px.png", Text="Main Page", Description=""},
                                                                 new Models.MenuItem() {ID=2, Logo="List_50px.png", Text="Academy Winners / Nominees", Description="" },
                                                                 new Models.MenuItem() {ID=3, Logo="StarFilled_48px.png", Text="My Favorites", Description="" },
                                                                 new Models.MenuItem() {ID=4, Logo="BarChart_52px.png", Text="Awards Polls", Description="" },
                                                                 //new Models.MenuItem() {ID=5, Logo="Edit_48px.png", Text="My Reviews", Description="" },
                                                                 new Models.MenuItem() {ID=6, Logo="Rocket_48px.png", Text="Academy Awards Trivia", Description="" },
                                                                 //new Models.MenuItem() {ID=7, Logo="Unsplash_48px.png", Text="Take a Picture Holding an Oscar", Description="" },
                                                                 new Models.MenuItem() {ID=8, Logo="Account_50px.png", Text="Edit Profile", Description="" },
                                                                 new Models.MenuItem() {ID=9, Logo="SignOut_50px.png", Text="Sign Out", Description="" }
                                                                  };

        }
        #endregion


        #region Commands

        private async void MenuItemClicked(int menuID)
        {
            if (menuID == 1)
            {
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MasterDetailContent());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 2)
            {
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new CategoriesFilterView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;

            }
            if (menuID == 3)
            {
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MyFavoritesPage());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 4)
            {
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AwardsPollsView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 5)
            {
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MyReviewsView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 6)
            {
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AcademyAwardsTriviaView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 7)
            {
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MovieDetailsView());
                //await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TakePictureOscarView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 8)
            {

                ViewModelFactory.GetInstance<UserViewModel>().SetEditValuesCurrentUser();

                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new EditProfileView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 9)
            {
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new SignOutView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
        }

        public ICommand MenuItemClickedCommand
        {
            get;
            set;
        }
        public ICommand ToolbarItemCommand
        { get; set; }
        private void ToolbarAboutView()
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AboutAppView());
            ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
        }

        #endregion
    }
}