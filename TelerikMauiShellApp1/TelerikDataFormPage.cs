using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using Telerik.Maui;
using Telerik.Maui.Controls;

namespace TelerikMauiShellApp1;

public partial class TelerikDataFormPage : ContentPage
{


    partial class ViewModel : ObservableObject {
		[ObservableProperty]
        [Display(Name = "Some string entry")]
        string text;

		public enum Options { 
			Option1, Option2, Option3
		}

		[ObservableProperty]
        [Display(Name = "Some option picker")]
        Options selectedOption;

		[ObservableProperty]
		[Display(Name = "Some flag")]
		bool flag;
    }

    public TelerikDataFormPage()
	{
        // DataFormLocalizationManager.Manager = DataFormResources.ResourceManager;

        var dataForm = new RadDataForm();
		dataForm.BindingContext = new ViewModel();

        Content = new VerticalStackLayout {
			Children = {
                
                /*
				new Button()
					.Text("Toggle locale")
					.Invoke(b => b.Clicked += (s,e) => {
                        TelerikLocalizationManager.Manager.
                    })
					,
                */
				dataForm
            }
		};
	}
}