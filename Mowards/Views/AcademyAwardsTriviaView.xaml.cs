using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mowards.Services;
using Mowards.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mowards.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AcademyAwardsTriviaView : ContentPage
	{
		public AcademyAwardsTriviaView ()
		{
			InitializeComponent ();
            BindingContext = ViewModelFactory.GetInstance<TriviaViewModel>();
		}
	}
}