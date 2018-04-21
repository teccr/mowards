using System;
using System.ComponentModel;

namespace Mowards.ViewModels
{
    /// <summary>
    /// Base class to repreent a Mutable data object. 
    /// Example of usage: ViewModels, nested collections in models, etc.
    /// </summary>
    public abstract class MutableDataObject
        : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Interface Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName) && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
