using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Mowards.Services;
using Mowards.ViewModels;
using Realms;
using Mowards.Models;

namespace Mowards
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }

    public partial class App : Application
    {
        #region Auth Code

        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        #endregion

        public App()
        {
            InitializeComponent();

            var realmInstance = Realm.GetInstance(Utils.REALM_DB_NAME);
            var localUserData = realmInstance.All<LoginInfo>();
            if (localUserData.Count() > 0)
            {
                var firstUser = localUserData.FirstOrDefault();
                if (!string.IsNullOrEmpty(firstUser.Username))
                {
                    if (App.Current.Properties.ContainsKey(Utils.TOKEN_KEY))
                    {
                        App.Current.Properties[Utils.TOKEN_KEY] = firstUser.Token;
                    }
                    else
                    {
                        App.Current.Properties.Add(Utils.TOKEN_KEY, firstUser.Token);
                    }
                }
                var userViewModel = ViewModelFactory.GetInstance<UserViewModel>();
                userViewModel.CurrentUser = new MowardsUser() { Email = firstUser.Username } ;
                MainPage = new Views.MasterDetailMaster();
            }
            else
            {
                MainPage = new Views.LoginView();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
