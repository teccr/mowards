using System;
using System.Collections.Generic;
using Mowards.Services;
using Mowards.ViewModels;
using Xamarin.Forms;

namespace Mowards.Views
{
    public partial class TriviaQuestionsPanel : ContentPage
    {
        public TriviaQuestionsPanel()
        {
            InitializeComponent();
            BindingContext = ViewModelFactory.GetInstance<TriviaViewModel>();
        }
    }
}
