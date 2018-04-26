using System;
using System.Collections.Generic;
using Mowards.ViewModels;
using Xamarin.Forms;

namespace Mowards.Views
{
    public partial class CategoriesFilterView : ContentPage
    {
        public CategoriesFilterView()
        {
            InitializeComponent();

            BindingContext = AwardsViewModel.GetInstance();
        }
    }
}
