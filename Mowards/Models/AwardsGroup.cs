using System;
using System.Collections.ObjectModel;

namespace Mowards.Models
{
    public class AwardsGroup : ObservableCollection<Award>
    {
        public string Name 
        {
            get;
            private set;
        }
        public string ShortName
        {
            get; 
            private set;
        }
    }
}
