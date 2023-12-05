using ProductInformationApp.Services;
using Prins;
using System.Collections.Immutable;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;

namespace ProductInformationApp
{
    partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        Product product;

        public MainPageViewModel(Product product)
        {
            Product = product;
        }
    }
}