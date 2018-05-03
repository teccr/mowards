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
	public partial class EditProfileView : ContentPage
	{
		public EditProfileView ()
		{
			InitializeComponent ();
            BindingContext = ViewModelFactory.GetInstance<AwardsViewModel>();
        }
	}
}