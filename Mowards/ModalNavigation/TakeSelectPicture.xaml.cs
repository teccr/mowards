using Mowards.Services;
using Mowards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mowards.ModalNavigation
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TakeSelectPicture : ContentPage
	{
		public TakeSelectPicture ()
		{
			InitializeComponent ();
            BindingContext = ViewModelFactory.GetInstance<UserViewModel>();
        }
	}
}