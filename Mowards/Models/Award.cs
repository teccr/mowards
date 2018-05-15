using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mowards.MowardsService;

namespace Mowards.Models
{
    public class Award
    {
        public string Id
        {
            get;
            set;
        }
        public int Year { get; set; }
        public int Awards_of_Year { get; set; }
        public string Ceremony_Number { get; set; }
        public string Category { get; set; }
        public string Nominee { get; set; }
        public string Additional_Info { get; set; }
        public int TMDBMovieID { get; set; }
        public string TMDBTitle { get; set; }
        public int SearchableYear { get; set; }
        public string Type { get; set; }
        public string AwardPosterPath { get; set; }

        public int Won { get; set; }

        public static async Task<List<Award>> GetAwardsByFilters(
            int year, string[] categories)
        {
            string url = $"?year={year.ToString()}";
            foreach(var category in categories)
            {
                url = url + $"&categories[]={System.Net.WebUtility.UrlEncode(category)}";
            }
            url = Utils.AWARDS_CONTROLLER + url;
            MowardsHttp client = new MowardsHttp();
            return await client.Get<List<Award>>(url);
        }


        public string NameSort
        {
            get
            {
                if (string.IsNullOrEmpty(Category) || Category.Length == 0)
                    return "?";
                return Category.ToUpper();
            }
        }
    }
}
