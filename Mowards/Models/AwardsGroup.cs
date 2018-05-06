using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mowards.Models
{
    public class AwardsGroup<K,T> : ObservableCollection<T>
    {
        public K Key
        {
            get;
            set;
        }

        public AwardsGroup(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
            {
                this.Items.Add(item);
            }
        }
    }
}
