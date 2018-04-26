using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mowards.Models;

namespace Mowards.ViewModels
{
    public class AwardsViewModel : ViewModelBase
    {
        #region Singleton Definition

        private static AwardsViewModel instance = null;

        public static AwardsViewModel GetInstance()
        {
            if (instance == null)
            {

                instance = new AwardsViewModel();
            }

            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
            {
                instance = null;
            }
        }

        public AwardsViewModel()
        {
            InitClass();
            InitCommands();
        }

        #endregion

        protected override async void InitClass()
        {
            Categories = await AwardCategory.GetAwardsCategories();
        }

        protected override void InitCommands()
        {

        }

        #region Data properties

        private ObservableCollection<AwardCategory> _categories;
        public ObservableCollection<AwardCategory> Categories
        {
            get 
            {
                return _categories;
            }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        #endregion
    }
}
