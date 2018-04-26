using System;
using Mowards.Models;
using Mowards.MowardsService;

namespace Mowards.ViewModels
{
    public abstract class ViewModelBase : MutableDataObject
    {
        #region Common Functions

        protected abstract void InitCommands();

        protected abstract void InitClass();
        #endregion

        #region Commun Objects

        private MowardsHttp _httpClient;
        internal MowardsHttp HttpClient
        {
            get
            {
                if (_httpClient == null)
                    _httpClient = new MowardsHttp();
                return _httpClient;
            }
        }

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

        protected void HandleException(Exception exception)
        {
            
        }

    }
}
