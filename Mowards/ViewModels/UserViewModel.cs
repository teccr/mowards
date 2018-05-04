using Mowards.Models;
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
           
        }

        protected override void InitCommands()
        {
            SaveEditUserCommand = new Command(SaveEditUser);
            CancelEditUserCommand = new Command(CancelCurrentView);
           
        }
        private void SaveEditUser()
        {

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

        public ICommand RemoveFavoriteCommand
        { get; set; }
        public ICommand EditReviewCommand
        { get; set; }
        public ICommand RemoveReviewCommand
        { get; set; }
        #endregion


    }
}
