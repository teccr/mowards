using System;
using System.Collections.ObjectModel;
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
        public LoginViewModel()
        {
            InitClass();
            InitCommands();
        }

        #region Initialization section

        protected override void InitClass()
        {

        }

        protected override void InitCommands()
        {
            LoginCommand = new Command(Login);
            RegisterNewUserViewCommand = new Command(RegisterNewUserView);
            RegisterNewUserCommand = new Command(RegisterNewUserAction);
            CancelRegisterUserCommand = new Command(CancelRegister);
        }

        #endregion

        #region Data Login

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

        #region Data Register New User
        private ObservableCollection<String> _listOfCountries;
        public ObservableCollection<String> ListOfCountries {
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
        private string _newUserPicture;
        public string NewUserPicture
        {
            get
            {
                return _newUserPicture;
            }
            set
            {
                _newUserPicture = value;
                OnPropertyChanged("NewUserPicture");
            }
        }

        private string _newUserEmail;
        public string NewUserEmail
        {
            get
            {
                return _newUserEmail;
            }
            set
            {
                _newUserEmail = value;
                OnPropertyChanged("NewUserEmail");
            }
        }

        private string _newUserName;
        public string NewUserName
        {
            get
            {
                return _newUserName;
            }
            set
            {
                _newUserName = value;
                OnPropertyChanged("NewUserName");
            }
        }

        private string _newUserPassword;
        public string NewUserPassword
        {
            get
            {
                return _newUserPassword;
            }
            set
            {
                _newUserPassword = value;
                OnPropertyChanged("NewUserPassword");
            }
        }

        private string _newUserPasswordConfirm;
        public string NewUserPasswordConfirm
        {
            get
            {
                return _newUserPasswordConfirm;
            }
            set
            {
                _newUserPasswordConfirm = value;
                OnPropertyChanged("NewUserPasswordConfirm");
            }
        }


        private DateTime _newUserbirthDate;
        public DateTime NewUserBirthDate
        {
            get
            {
                return _newUserbirthDate;
            }
            set
            {
                _newUserbirthDate = value;
                OnPropertyChanged("NewUserBirthDate");
            }
        }

        private string _newUserCountry;
        public string NewUserCountry
        {
            get
            {
                return _newUserCountry;
            }
            set
            {
                _newUserCountry = value;
                OnPropertyChanged("NewUserCountry");
            }
        }

        #endregion
        #region Commands

        public ICommand LoginCommand
        {
            get;
            set;
        }
        public ICommand RegisterNewUserViewCommand
        {
            get;
            set;
        }
        public ICommand RegisterNewUserCommand
        {
            get;
            set;
        }
        public ICommand CancelRegisterUserCommand
        {
            get;
            set;
        }
        private async void Login()
        {
            Func<Task> loginOperation = async () =>
            {
                LoginInfo userCredentials = new LoginInfo()
                {
                    Username = Username,
                    Password = Password
                };

                var tokenInformation =
                    await HttpClient.PostDetails<TokenInformation, LoginInfo>(
                    Utils.AUTH_CONTROLLER, userCredentials, true);

                if (App.Current.Properties.ContainsKey(Utils.TOKEN_KEY))
                {
                    App.Current.Properties[Utils.TOKEN_KEY] = tokenInformation.Token;
                }
                else
                {
                    App.Current.Properties.Add(Utils.TOKEN_KEY, tokenInformation.Token);
                }
                App.Current.MainPage = new CategoriesFilterView();
            };

            await ExecuteSafeOperation(loginOperation);
        }

        private async void RegisterNewUserView()
        {
            if (ListOfCountries == null || ListOfCountries.Count==0) {
                
                await SetListCountries();
            }

            //(App.Current.MainPage).Navigation.PushAsync(new RegisterNewUserView());
            App.Current.MainPage = new Mowards.Views.RegisterNewUserView();
        }

        private async void RegisterNewUserAction() {

        }
        private async void CancelRegister()
        {

        }

        private async Task SetListCountries() {

            ListOfCountries = new ObservableCollection<string>() {
                "AFGHANISTAN","ÅLAND ISLANDS","ALBANIA","ALGERIA","AMERICAN SAMOA","ANDORRA","ANGOLA","ANGUILLA","ANTARCTICA","ANTIGUA AND BARBUDA","ARGENTINA","ARMENIA","ARUBA","AUSTRALIA","AUSTRIA","AZERBAIJAN","BAHAMAS","BAHRAIN","BANGLADESH","BARBADOS","BELARUS","BELGIUM","BELIZE","BENIN","BERMUDA","BHUTAN","BOLIVIA","BOSNIA AND HERZEGOVINA","BOTSWANA","BOUVET ISLAND","BRAZIL","BRITISH INDIAN OCEAN TERRITORY","BRITISH VIRGIN ISLANDS","BRUNEI","BULGARIA","BURKINA FASO","BURUNDI","CAMBODIA","CAMEROON","CANADA","CAPE VERDE","CAYMAN ISLANDS","CENTRAL AFRICAN REPUBLIC","CHAD","CHILE","CHINA","CHRISTMAS ISLAND","COCOS ISLANDS","COLOMBIA","COMOROS","CONGO","COOK ISLANDS","COSTA RICA","CÔTE D’IVOIRE","CROATIA","CUBA","CURAÇAO","CYPRUS","CZECH REPUBLIC","DENMARK","DJIBOUTI","DOMINICA","DOMINICAN REPUBLIC","ECUADOR","EGYPT","EL SALVADOR","EQUATORIAL GUINEA","ERITREA","ESTONIA","ETHIOPIA","FALKLAND ISLANDS","FAROE ISLANDS","FIJI","FINLAND","FRANCE","FRENCH GUIANA","FRENCH POLYNESIA","FRENCH SOUTHERN TERRITORIES","GABON","GAMBIA","GEORGIA","GERMANY","GHANA","GIBRALTAR","GREECE","GREENLAND","GRENADA","GUADELOUPE","GUAM","GUATEMALA","GUERNSEY","GUINEA","GUINEA-BISSAU","GUYANA","HAITI","HEARD ISLAND AND MCDONALD ISLANDS","HONDURAS","HONG KONG","HUNGARY","ICELAND","INDIA","INDONESIA","IRAN","IRAQ","IRELAND","ISLE OF MAN","ISRAEL","ITALY","JAMAICA","JAPAN","JERSEY","JORDAN","KAZAKHSTAN","KENYA","KIRIBATI","KUWAIT","KYRGYZSTAN","LAOS","LATVIA","LEBANON","LESOTHO","LIBERIA","LIBYA","LIECHTENSTEIN","LITHUANIA","LUXEMBOURG","MACAO","MACEDONIA","MADAGASCAR","MALAWI","MALAYSIA","MALDIVES","MALI","MALTA","MARSHALL ISLANDS","MARTINIQUE","MAURITANIA","MAURITIUS","MAYOTTE","MEXICO","MICRONESIA","MOLDOVA","MONACO","MONGOLIA","MONTENEGRO","MONTSERRAT","MOROCCO","MOZAMBIQUE","MYANMAR","NAMIBIA","NAURU","NEPAL","NETHERLANDS","NETHERLANDS ANTILLES","NEW CALEDONIA","NEW ZEALAND","NICARAGUA","NIGER","NIGERIA","NIUE","NORFOLK ISLAND","NORTHERN MARIANA ISLANDS","NORTH KOREA","NORWAY","OMAN","PAKISTAN","PALAU","PALESTINE","PANAMA","PAPUA NEW GUINEA","PARAGUAY","PERU","PHILIPPINES","PITCAIRN","POLAND","PORTUGAL","PUERTO RICO","QATAR","REUNION","ROMANIA","RUSSIA","RWANDA","SAINT BARTHÉLEMY","SAINT HELENA","SAINT KITTS AND NEVIS","SAINT LUCIA","SAINT MARTIN","SAINT PIERRE AND MIQUELON","SAINT VINCENT AND THE GRENADINES","SAMOA","SAN MARINO","SAO TOME AND PRINCIPE","SAUDI ARABIA","SENEGAL","SERBIA","SEYCHELLES","SIERRA LEONE","SINGAPORE","SINT MAARTEN (DUTCH PART)","SLOVAKIA","SLOVENIA","SOLOMON ISLANDS","SOMALIA","SOUTH AFRICA","SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS","SOUTH KOREA","SOUTH SUDAN","SPAIN","SRI LANKA","SUDAN","SURINAME","SVALBARD AND JAN MAYEN","SWAZILAND","SWEDEN","SWITZERLAND","SYRIA","TAIWAN","TAJIKISTAN","TANZANIA","THAILAND","THE DEMOCRATIC REPUBLIC OF CONGO","TIMOR-LESTE","TOGO","TOKELAU","TONGA","TRINIDAD AND TOBAGO","TUNISIA","TURKEY","TURKMENISTAN","TURKS AND CAICOS ISLANDS","TUVALU","U.S. VIRGIN ISLANDS","UGANDA","UKRAINE","UNITED ARAB EMIRATES","UNITED KINGDOM","UNITED STATES","UNITED STATES MINOR OUTLYING ISLANDS","URUGUAY","UZBEKISTAN","VANUATU","VATICAN","VENEZUELA","VIETNAM","WALLIS AND FUTUNA","WESTERN SAHARA","YEMEN","ZAMBIA","ZIMBABWE"
            };

            
        }
        #endregion
    }
}
