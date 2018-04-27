using System;
using System.Collections.Generic;
using Mowards.Services;
using Mowards.ViewModels;
using Xamarin.Forms;

namespace Mowards.Views
{
    public partial class CategoriesFilterView : ContentPage
    {
        public CategoriesFilterView()
        {
            InitializeComponent();

            BindingContext = ViewModelFactory.GetInstance<AwardsViewModel>();
        }

        async void YearSlider_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if(BindingContext != null)
            {
                AwardsViewModel viewModel = (AwardsViewModel)BindingContext;
                await viewModel.LoadCategories();
            }
        }
    }
}
