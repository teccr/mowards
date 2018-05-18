using System;
using System.Collections.Generic;
using Mowards.Services;
using Mowards.ViewModels;
using Xamarin.Forms;

namespace Mowards.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
	}
}
