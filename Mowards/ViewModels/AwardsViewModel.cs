using System;
using System.Collections.ObjectModel;
using Mowards.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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
            SaveEditUserCommand = new Command(SaveEditUser);
            CancelEditUserCommand = new Command(CancelCurrentView);
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
        private string _EditUserNewPassword { get; set; }
        public string EditUserNewPassword {
                get {
                    return _EditUserNewPassword;
                } set {
                    _EditUserNewPassword = value;
                    OnPropertyChanged("EditUserNewPassword");
                }}
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
        {get;set;}
        public ICommand SaveEditUserCommand
        { get; set; }
        public ICommand CancelEditUserCommand
        { get; set; }

        public async Task LoadCategories()
        {
            Func<Task> operation = async () =>
            {
                Categories = await AwardCategory.GetAwardsCategoriesByYear(SelectedYear);
            };
            await ExecuteSafeOperation(operation);
        }

        #endregion
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
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TakePictureOscarView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 8)
            {

                SetListCountries();
                EditUserNewBirthday = CurrentUser.BirthDate;
                EditUserNewCountry = CurrentUser.Country;
                EditUserNewFullName = CurrentUser.Fullname;

                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new EditProfileView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
            if (menuID == 9)
            {
                await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new SignOutView());
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
        }
        private void SaveEditUser() {

        }
        private void CancelCurrentView() {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopAsync();
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
        private void SetListCountries()
        {

            ListOfCountries = new ObservableCollection<string>() {
                "AFGHANISTAN","ÅLAND ISLANDS","ALBANIA","ALGERIA","AMERICAN SAMOA","ANDORRA","ANGOLA","ANGUILLA","ANTARCTICA","ANTIGUA AND BARBUDA","ARGENTINA","ARMENIA","ARUBA","AUSTRALIA","AUSTRIA","AZERBAIJAN","BAHAMAS","BAHRAIN","BANGLADESH","BARBADOS","BELARUS","BELGIUM","BELIZE","BENIN","BERMUDA","BHUTAN","BOLIVIA","BOSNIA AND HERZEGOVINA","BOTSWANA","BOUVET ISLAND","BRAZIL","BRITISH INDIAN OCEAN TERRITORY","BRITISH VIRGIN ISLANDS","BRUNEI","BULGARIA","BURKINA FASO","BURUNDI","CAMBODIA","CAMEROON","CANADA","CAPE VERDE","CAYMAN ISLANDS","CENTRAL AFRICAN REPUBLIC","CHAD","CHILE","CHINA","CHRISTMAS ISLAND","COCOS ISLANDS","COLOMBIA","COMOROS","CONGO","COOK ISLANDS","COSTA RICA","CÔTE D’IVOIRE","CROATIA","CUBA","CURAÇAO","CYPRUS","CZECH REPUBLIC","DENMARK","DJIBOUTI","DOMINICA","DOMINICAN REPUBLIC","ECUADOR","EGYPT","EL SALVADOR","EQUATORIAL GUINEA","ERITREA","ESTONIA","ETHIOPIA","FALKLAND ISLANDS","FAROE ISLANDS","FIJI","FINLAND","FRANCE","FRENCH GUIANA","FRENCH POLYNESIA","FRENCH SOUTHERN TERRITORIES","GABON","GAMBIA","GEORGIA","GERMANY","GHANA","GIBRALTAR","GREECE","GREENLAND","GRENADA","GUADELOUPE","GUAM","GUATEMALA","GUERNSEY","GUINEA","GUINEA-BISSAU","GUYANA","HAITI","HEARD ISLAND AND MCDONALD ISLANDS","HONDURAS","HONG KONG","HUNGARY","ICELAND","INDIA","INDONESIA","IRAN","IRAQ","IRELAND","ISLE OF MAN","ISRAEL","ITALY","JAMAICA","JAPAN","JERSEY","JORDAN","KAZAKHSTAN","KENYA","KIRIBATI","KUWAIT","KYRGYZSTAN","LAOS","LATVIA","LEBANON","LESOTHO","LIBERIA","LIBYA","LIECHTENSTEIN","LITHUANIA","LUXEMBOURG","MACAO","MACEDONIA","MADAGASCAR","MALAWI","MALAYSIA","MALDIVES","MALI","MALTA","MARSHALL ISLANDS","MARTINIQUE","MAURITANIA","MAURITIUS","MAYOTTE","MEXICO","MICRONESIA","MOLDOVA","MONACO","MONGOLIA","MONTENEGRO","MONTSERRAT","MOROCCO","MOZAMBIQUE","MYANMAR","NAMIBIA","NAURU","NEPAL","NETHERLANDS","NETHERLANDS ANTILLES","NEW CALEDONIA","NEW ZEALAND","NICARAGUA","NIGER","NIGERIA","NIUE","NORFOLK ISLAND","NORTHERN MARIANA ISLANDS","NORTH KOREA","NORWAY","OMAN","PAKISTAN","PALAU","PALESTINE","PANAMA","PAPUA NEW GUINEA","PARAGUAY","PERU","PHILIPPINES","PITCAIRN","POLAND","PORTUGAL","PUERTO RICO","QATAR","REUNION","ROMANIA","RUSSIA","RWANDA","SAINT BARTHÉLEMY","SAINT HELENA","SAINT KITTS AND NEVIS","SAINT LUCIA","SAINT MARTIN","SAINT PIERRE AND MIQUELON","SAINT VINCENT AND THE GRENADINES","SAMOA","SAN MARINO","SAO TOME AND PRINCIPE","SAUDI ARABIA","SENEGAL","SERBIA","SEYCHELLES","SIERRA LEONE","SINGAPORE","SINT MAARTEN (DUTCH PART)","SLOVAKIA","SLOVENIA","SOLOMON ISLANDS","SOMALIA","SOUTH AFRICA","SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS","SOUTH KOREA","SOUTH SUDAN","SPAIN","SRI LANKA","SUDAN","SURINAME","SVALBARD AND JAN MAYEN","SWAZILAND","SWEDEN","SWITZERLAND","SYRIA","TAIWAN","TAJIKISTAN","TANZANIA","THAILAND","THE DEMOCRATIC REPUBLIC OF CONGO","TIMOR-LESTE","TOGO","TOKELAU","TONGA","TRINIDAD AND TOBAGO","TUNISIA","TURKEY","TURKMENISTAN","TURKS AND CAICOS ISLANDS","TUVALU","U.S. VIRGIN ISLANDS","UGANDA","UKRAINE","UNITED ARAB EMIRATES","UNITED KINGDOM","UNITED STATES","UNITED STATES MINOR OUTLYING ISLANDS","URUGUAY","UZBEKISTAN","VANUATU","VATICAN","VENEZUELA","VIETNAM","WALLIS AND FUTUNA","WESTERN SAHARA","YEMEN","ZAMBIA","ZIMBABWE"
            };
        }
        #endregion
    }
}
