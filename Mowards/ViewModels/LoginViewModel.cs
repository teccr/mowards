using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Mowards.Models;
using Mowards.Views;
using Xamarin.Forms;

namespace Mowards.ViewModels
{
    public class LoginViewModel :
        ViewModelBase
    {
        #region Singleton Definition

        private static LoginViewModel instance = null;

        public static LoginViewModel GetInstance()
        {
            if (instance == null)
            {

                instance = new LoginViewModel();
            }

            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
            {
                instance = null;
            }
        }

        public LoginViewModel()
        {
            InitClass();
            InitCommands();
        }

        #endregion

        #region Initialization section

        protected override void InitClass()
        {

        }

        protected override void InitCommands()
        {
            LoginCommand = new Command(Login);
        }

        #endregion

        #region Data

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        private string _password;
        public string Password
        {
            get 
            {
                return _password;
            }
            set 
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        #endregion

        #region Commands

        public ICommand LoginCommand
        {
            get;
            set;
        }

        private async void Login()
        {
            if (IsBusy)
                return;
            try
            {
                LoginInfo userCredentials = new LoginInfo() 
                { 
                    Username = Username,
                    Password = Password
                };

                var tokenInformation =
                    await HttpClient.PostDetails<TokenInformation, LoginInfo>(
                    Utils.AUTH_CONTROLLER, userCredentials, true);

                if(App.Current.Properties.ContainsKey(Utils.TOKEN_KEY))
                {
                    App.Current.Properties[Utils.TOKEN_KEY] = tokenInformation.Token;
                }
                else
                {
                    App.Current.Properties.Add( Utils.TOKEN_KEY, tokenInformation.Token );
                }
                App.Current.MainPage = new CategoriesFilterView();
            }
            catch(Exception excep)
            {
                HandleException(excep);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}
