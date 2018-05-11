using Mowards.Models;
using Mowards.MowardsService;
using Mowards.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mowards.ViewModels
{
    public class UserViewModel : ViewModelBase
    {

        #region Initialization

        public UserViewModel()
        {
            InitClass();
            InitCommands();
          
        }

        protected override void InitClass()
        {
            ListOfCountries= ViewModelFactory.GetInstance<LoginViewModel>().SetListCountries();
        }

        protected override void InitCommands()
        {
            SaveEditUserCommand = new Command(SaveEditUser);
            CancelEditUserCommand = new Command(CancelCurrentView);
            ResetPasswordCommand=new Command(ResetPassword);
        }

        private async void ResetPassword(object obj)
        {
            MowardsHttp client = new MowardsHttp();

            string result = "";
           
            string url = $"?password={EditUserNewPassword}";
            url = Utils.USER_URL + url;
            if (EditUserNewPassword == "")
            {
                result = "Password cant be blank";
            }
            else { 
                if (EditUserNewPassword == EditUserNewPasswordConfirm) {
                    result = await client.Put<string>(
                   url);
                    EditUserNewPassword = "";
                    EditUserNewPasswordConfirm = "";
                }
                else{
                    result = "Password and Password Confirmation does not match";
                }
            }
            await App.Current.MainPage.DisplayAlert("Result", result, "OK");
            

        }

        private async void SaveEditUser()
        {
            MowardsHttp client = new MowardsHttp();

          
            string url = Utils.USER_URL;
            MowardsUser usr = new MowardsUser() { Email = CurrentUser.Email, Fullname=EditUserNewFullName  , BirthDate=EditUserNewBirthday, Country=EditUserNewCountry };

            CurrentUser = await client.Put<MowardsUser>(
                url, usr);
            CurrentUser.Picture = "User_104px.png";
            await App.Current.MainPage.DisplayAlert("Result", CurrentUser.Email+ " has been edited!", "OK");
            SetEditValuesCurrentUser();
        }

        #endregion
        #region Instances
       
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
        private ObservableCollection<String> _listOfCountries;
        public ObservableCollection<String> ListOfCountries
        {
            get
            {
                return _listOfCountries;
            }
            set
            {
                _listOfCountries = value;
                OnPropertyChanged("ListOfCountries");
            }
        }
        public async Task<MowardsUser> SetUserFullInformation(string email) {
            return await GetUserFullInformationAsync(email);
            
        }
        private async Task<MowardsUser> GetUserFullInformationAsync(string email)
        {
            MowardsHttp client = new MowardsHttp();
           
            string url = $"?username={email}";
            url = Utils.USER_URL + url;
            CurrentUser =await client.Get<MowardsUser>(
                url);
            CurrentUser.Picture = "User_104px.png";
            return CurrentUser;
        }
        #endregion
        #region Reviews and Favorites
        private ObservableCollection<Review> _UserReviews { get; set; }
        public ObservableCollection<Review> UserReviews
        {
            get
            {
                return _UserReviews;
            }

            set { _UserReviews = value; OnPropertyChanged("UserReviews"); }
        }

        private ObservableCollection<Favorite> _UserFavorites { get; set; }
        public ObservableCollection<Favorite> UserFavorites
        {
            get
            {
                return _UserFavorites;
            }

            set { _UserFavorites = value; OnPropertyChanged("UserFavorites"); }
        }
        #endregion
        #region Edit User View Properties


        private string _EditUserNewPassword { get; set; }
        public string EditUserNewPassword
        {
            get
            {
                return _EditUserNewPassword;
            }
            set
            {
                _EditUserNewPassword = value;
                OnPropertyChanged("EditUserNewPassword");
            }
        }
        private string _EditUserNewPasswordConfirm { get; set; }
        public string EditUserNewPasswordConfirm
        {
            get
            {
                return _EditUserNewPasswordConfirm;
            }
            set
            {
                _EditUserNewPasswordConfirm = value;
                OnPropertyChanged("EditUserNewPasswordConfirm");
            }
        }
        private DateTime _EditUserNewBirthday { get; set; }
        public DateTime EditUserNewBirthday
        {
            get
            {
                return _EditUserNewBirthday;
            }
            set
            {
                _EditUserNewBirthday = value;
                OnPropertyChanged("EditUserNewBirthday");
            }
        }
        private string _EditUserNewCountry { get; set; }
        public string EditUserNewCountry
        {
            get
            {
                return _EditUserNewCountry;
            }
            set
            {
                _EditUserNewCountry = value;
                OnPropertyChanged("EditUserNewCountry");
            }
        }
        private string _EditUserNewFullName { get; set; }
        public string EditUserNewFullName
        {
            get
            {
                return _EditUserNewFullName;
            }
            set
            {
                _EditUserNewFullName = value;
                OnPropertyChanged("EditUserNewFullName");
            }
        }

        #endregion
        #region Edit User Helper Methods
        public void SetEditValuesCurrentUser()
        {
            EditUserNewBirthday = CurrentUser.BirthDate;
            EditUserNewCountry = CurrentUser.Country;
            EditUserNewFullName = CurrentUser.Fullname;
        }
        #endregion
        #region Commands
        public ICommand SaveEditUserCommand
        { get; set; }
        public ICommand CancelEditUserCommand
        { get; set; }
        public ICommand ResetPasswordCommand
        { get; set; }

        public ICommand RemoveFavoriteCommand
        { get; set; }
        public ICommand EditReviewCommand
        { get; set; }
        public ICommand RemoveReviewCommand
        { get; set; }
        #endregion


    }
}
