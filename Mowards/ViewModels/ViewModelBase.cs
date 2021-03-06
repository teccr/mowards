﻿using System;
using System.Threading.Tasks;
using Mowards.Models;
using Mowards.MowardsService;
using Xamarin.Forms;

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
        /// <summary>
        /// If implemented in the XAML UI, it will display loading animations.
        /// </summary>
        /// <value><c>true</c> if is busy; otherwise, <c>false</c>.</value>
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

        /// <summary>
        /// Pipeline to handle exceptions in View Model
        /// </summary>
        /// <param name="exception">Exception.</param>
        protected async Task HandleException(Exception exception)
        {
            await App.Current.MainPage.DisplayAlert("Error", exception.Message, "OK");
        }

        /// <summary>
        /// Allows to execute a void function in a safe context.
        /// IsBusy property will be setup and there is a try/catch system managing exceptions.
        /// </summary>
        /// <returns>The safe operation.</returns>
        /// <param name="operation">Function to be executed.</param>
        /// <param name="setBusyProperty">Default true. If true, the IsBusy property will be setup to true during execution and false in the end.</param>
        protected async Task ExecuteSafeOperation(Func<Task> operation, 
                                                  bool setBusyProperty = true)
        {
            try
            {
                if(setBusyProperty) 
                    IsBusy = true;
                await operation();
            }
            catch (Exception excep)
            {
                await HandleException(excep);
            }
            finally
            {
                if(setBusyProperty)
                    IsBusy = false;
            }
        }

        protected async void CancelCurrentView()
        {
            await ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopAsync();
        }
    }
}
