using System;
namespace Mowards.ViewModels
{
    public abstract class ViewModelBase : MutableDataObject
    {
        #region Common Functions

        protected abstract void InitCommands();

        protected abstract void InitClass();
        #endregion

        #region Common Properties

        private bool _isBusy = false;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        #endregion
    }
}
