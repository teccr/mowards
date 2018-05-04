using System;
using System.Collections.Generic;
using Mowards.Services;
using Mowards.ViewModels;
using Xamarin.Forms;

namespace Mowards.Views
{
    public partial class FilteredAwardsView : ContentPage
    {
        public FilteredAwardsView()
        {
            InitializeComponent();
            BindingContext = ViewModelFactory.GetInstance<AwardsViewModel>();
        }
    }
}
