using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Mowards.MowardsService;
using Mowards.ViewModels;

namespace Mowards.Models
{
    public class AwardCategory : MutableDataObject
    {
        public string Id
        {
            get;
            set;
        }
        public int Year { get; set; }

        public string Category { get; set; }

        private bool _selected;
        public bool Selected 
        { 
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }

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
