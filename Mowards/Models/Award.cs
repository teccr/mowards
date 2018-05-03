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
        public int Won { get; set; }

        public static async Task<ObservableCollection<Award>> GetAwardsByFilters(
            int year, string[] categories)
        {
            string url = $"?year={year.ToString()}";
            foreach(var category in categories)
            {
                url = url + $"&categories[]={category}";
            }
            url = Utils.AWARDS_CONTROLLER + url;
            MowardsHttp client = new MowardsHttp();
            return await client.Get<ObservableCollection<Award>>(url);
        }
    }
}
