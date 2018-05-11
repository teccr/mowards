using Mowards.Models;
using Mowards.MowardsService;
using Mowards.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Mowards.ModalNavigation;
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
            TakeSelectPictureViewCommand = new Command(ShowTakeSelectPictureView);
            TakeCameraPictureCommand = new Command(TakeCameraPicture);
            SelectPictureCommand = new Command(SelectPicture);
            ConfirmPictureAndGoBackCommand = new Command(ConfirmPictureAndGoBack);
            CancelSelectImageCommand = new Command(CancelSelectImage);
            
       
       
         }
     
        private void CancelSelectImage()
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopModalAsync();
        }

        private async void ConfirmPictureAndGoBack()
        {
            if (ImageTakenPreview != null)
            {

                CurrentUser.Picture = ImageLocation;
                ViewModelFactory.GetInstance<MainMenuViewModel>().UpdateCurrentUserImage(ImageLocation);
                OnPropertyChanged("CurrentUser");
                //UPLOAD IMAGE TIED TO THIS USER to the Azure blob storage HERE
                 
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopModalAsync();
            }
            else {
                await App.Current.MainPage.DisplayAlert("Alert", "Please take or select an image first", "OK");
            }
           
        }

        private async void ShowTakeSelectPictureView(object obj)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new TakeSelectPicture());
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
            //CurrentUser.Picture = "User_104px.png";
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

        private Image _ImageTakenPreview { get; set; }
        public Image ImageTakenPreview
        {
            get { return _ImageTakenPreview; }
            set
            {
                _ImageTakenPreview = value;
                OnPropertyChanged("ImageTakenPreview");
            }
        }
        private Image _ImageUser { get; set; }
        public Image ImageUser
        {
            get { return _ImageUser; }
            set
            {
                _ImageUser = value;
                OnPropertyChanged("ImageUser");
            }

        }
        private string _ImageLocation { get; set; }
        public string ImageLocation
        {
            get { return _ImageLocation; }

            set
            {
                _ImageLocation = value;
                OnPropertyChanged("ImageLocation");
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
            //CurrentUser.Picture = "User_104px.png";
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
        #region Camera Async Methods
        private async void TakeCameraPicture()
        {


            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                
                await App.Current.MainPage.DisplayAlert("Error","No Camera Available", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 80,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front

            });

            if (file == null)
                return;

            //Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");
            ImageLocation = file.Path;
            ImageTakenPreview = new Image();
            ImageTakenPreview.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
            CurrentUser.Picture = ImageLocation;
           
            
        }
        private async void SelectPicture()
        {

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
        public ICommand TakeCameraPictureCommand
        { get; set; }
        public ICommand SelectPictureCommand
        { get; set; }
        public ICommand TakeSelectPictureViewCommand
        { get; set; }
        public ICommand ConfirmPictureAndGoBackCommand
        { get; set; }
        public ICommand CancelSelectImageCommand
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
