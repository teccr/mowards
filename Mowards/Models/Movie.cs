using Mowards.MowardsService;
using Mowards.Services;
using Mowards.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mowards.Models
{
    public class Movie : MutableDataObject
    {
        public string Id
        {
            get;
            set;
        }  

        public int TMDBMovieID { get; set; }
        public string TMDBTitle { get; set; }
        public string Overview { get; set; }
        public string PosterURL { get; set; }

        private MovieCredits _Credits { get; set; }
        public MovieCredits Credits
        { get { return _Credits; }
            set { _Credits = value; OnPropertyChanged("Credits"); } }

        private ObservableCollection<MovieCrew> _CrewList { get; set; }
        public ObservableCollection<MovieCrew> CrewList
        {
            get { return _CrewList; }
            set { _CrewList = value; OnPropertyChanged("CrewList"); }
        }
        public static async Task<Movie> GetMovieByTMDBId(int tmdbId)
        {
            MowardsHttp client = new MowardsHttp();
            //tables / Movies ? tmdbId = 21313
            string url = $"?tmdbId={tmdbId}";
            url = Utils.MOVIES_URL + url;
            Movie mv = await client.Get<Movie>(
                url);
            //CurrentUser.Picture = "User_104px.png";
            return mv;
        }
        public static async Task<MovieCredits> SetMovieCredits(int tmdbId)
        {

            TMDBHttp client = new TMDBHttp();

            //https://api.themoviedb.org/3/movie/399055/credits?api_key=9727dec007ba17df175696cbb6e051bc
            string url = "https://api.themoviedb.org/3/movie/";
            url += tmdbId;
            url += $"/credits?api_key=9727dec007ba17df175696cbb6e051bc";
           
            MovieCredits credits=
             await client.Get<MovieCredits>(
                url);
            return credits;
            //CurrentUser.Picture = "User_104px.png";
            //return mv;
        }

    }
}
