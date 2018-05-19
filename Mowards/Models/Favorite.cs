using Mowards.MowardsService;
using Mowards.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mowards.Models
{
    public class Favorite: MutableDataObject

    {
        public string Id { get;set;}
        public string MovieId { get; set; }
        public string MovieName { get; set; }
        public string Image { get; set; }
        public string Username
        {
            get;
            set;
        }

        public static async Task<ObservableCollection<Favorite>> GetUserFavorites()
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Get<ObservableCollection<Favorite>>(Utils.FAVORITES_URL);
        }

        public static async Task<ObservableCollection<Favorite>> GetUserFavorites(string movieId)
        { 
            MowardsHttp client = new MowardsHttp();
            return await client.Get<ObservableCollection<Favorite>>($"{Utils.FAVORITES_URL}?movieId={movieId}");
        }

        public static async Task<Favorite> SaveFavorite(Favorite fav)
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Post<Favorite>(Utils.FAVORITES_URL, fav);
        }

        public static async Task<Favorite> Delete(string id)
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Delete<Favorite>($"{Utils.FAVORITES_URL}?id={id}");
        }
    }
}
