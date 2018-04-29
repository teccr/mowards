using Xamarin.Forms;
using System.Threading.Tasks;

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

            
            MainPage = new Views.LoginView();
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
