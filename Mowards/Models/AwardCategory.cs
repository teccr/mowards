using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mowards.MowardsService;

namespace Mowards.Models
{
    public class AwardCategory
    {
        public string Id
        {
            get;
            set;
        }
        public int Year { get; set; }

        public string Category { get; set; }

        public bool Selected { get; set; }

        #region Model Operations

        public static async Task<ObservableCollection<AwardCategory>> GetAwardsCategories()
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Get<ObservableCollection<AwardCategory>>(
                Utils.AWARDSCATEGORIES_CONTROLLER);
        }

        public static async Task<ObservableCollection<AwardCategory>> GetAwardsCategoriesByYear(int year)
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Get<ObservableCollection<AwardCategory>>(
                Utils.AWARDSCATEGORIES_BY_YEAR_URL + year.ToString());
        }

        #endregion
    }
}
