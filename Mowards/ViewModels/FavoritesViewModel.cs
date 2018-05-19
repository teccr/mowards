using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Mowards.Models;
using System.Linq;
using Xamarin.Forms;

namespace Mowards.ViewModels
{
    public class FavoritesViewModel : ViewModelBase
    {
        #region Initialization

        public FavoritesViewModel()
        {
            InitClass();
            InitCommands();
        }

        protected override void InitClass()
        {
            MyFavorites = new ObservableCollection<Favorite>();
            GetMyReviews();
        }

        protected override void InitCommands()
        {
            GetMyReviewsCommand = new Command(GetMyReviews);
            RemoveFavoriteCommand = new Command<string>(RemoveFavorite);
        }

        #endregion

        #region Commands

        public ICommand RemoveFavoriteCommand
        {
            get;
            set;
        }

        private async void RemoveFavorite(string id)
        {
            Func<Task> deleteOp = new Func<Task>(
                async () =>
                {
                    this.MyFavorites.Remove( this.MyFavorites.Where( fav => fav.Id == id).FirstOrDefault() );
                    await Favorite.Delete(id);
                }
            );

            await ExecuteSafeOperation(deleteOp);
        }

        public ICommand GetMyReviewsCommand
        {
            get;
            set;
        }

        public async void GetMyReviews()
        {
            Func<Task> myReviews = new Func<Task>(
                async () => 
                {
                    MyFavorites = await Favorite.GetUserFavorites();
                }
            );

            await ExecuteSafeOperation(myReviews);
        }

        #endregion

        #region Properties

        private ObservableCollection<Favorite> _myFavorites;
        public ObservableCollection<Favorite> MyFavorites
        {
            get
            {
                return _myFavorites;
            }
            set
            {
                _myFavorites = value;
                OnPropertyChanged("MyFavorites");
            }
        }

        #endregion
    }
}
