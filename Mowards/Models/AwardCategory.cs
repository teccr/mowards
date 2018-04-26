using System;
using System.Collections.Generic;
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

        #region Model Operations

        public static async Task<ObservableCollection<AwardCategory>> GetAwardsCategories()
        {
            MowardsHttp client = new MowardsHttp();
            return await client.Get<ObservableCollection<AwardCategory>>(
                Utils.AWARDSCATEGORIES_CONTROLLER);
        }

        #endregion
    }
}
