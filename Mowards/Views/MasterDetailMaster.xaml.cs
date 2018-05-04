using Mowards.Services;
using Mowards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mowards.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterDetailMaster : MasterDetailPage
    {
		public MasterDetailMaster ()
		{
			InitializeComponent ();
            BindingContext = ViewModelFactory.GetInstance<MainMenuViewModel>();
        }

        /*async void YearSlider_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if (BindingContext != null)
            {
                AwardsViewModel viewModel = (AwardsViewModel)BindingContext;
                await viewModel.LoadCategories();
            }
        }*/
    }
}