using ProductInformationApp.Services;
using Prins;
using System.Collections.Immutable;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ProductInformationApp
{
    partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Overview))]
        [NotifyPropertyChangedFor(nameof(CommonInformation))]
        [NotifyPropertyChangedFor(nameof(Hints))]
        [NotifyPropertyChangedFor(nameof(TradingUnit))]
        [NotifyPropertyChangedFor(nameof(UnitPack))]
        [NotifyPropertyChangedFor(nameof(Palette))]
        [NotifyPropertyChangedFor(nameof(Ingredients))]
        [NotifyPropertyChangedFor(nameof(NutrientAnnotation))]
        [NotifyPropertyChangedFor(nameof(Nutrients))]
        [NotifyPropertyChangedFor(nameof(Additives))]
        [NotifyPropertyChangedFor(nameof(Alergens))]
        Product product;

        public record KeyValue(string Key, object Value);
        public record KeyValueUnit(string Key, object Value, string Unit);

        public ImmutableList<KeyValue> Overview 
        { 
            get => Product.GetOverview().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList(); 
        }

        public ImmutableList<KeyValue> CommonInformation
        { 
            get => Product.GetCommonInformation().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList(); 
        }

        public ImmutableList<KeyValue> Hints
        { 
            get => Product.GetHints().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList(); 
        }

        public ImmutableList<KeyValue> TradingUnit
        { 
            get => Product.GetTradingUnit().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList();
        }

        public ImmutableList<KeyValue> UnitPack
        { 
            get => Product.GetUnitPack().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList();
        }

        public ImmutableList<KeyValue> Palette
        { 
            get => Product.GetPaletteInfo().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList();
        }

        public string Ingredients { get => Product?.Ingredients?.Value ?? "-"; }

        public string NutrientAnnotation { get => Product?.NutrientAnnotation?.Value ?? "-"; }

        public ImmutableList<KeyValueUnit> Nutrients 
        { 
            get => Product.GetNutrients().Select(o => new KeyValueUnit(o.Item1, o.Item2, o.Item3)).ToImmutableList();
        }

        public ImmutableList<KeyValue> Additives 
        { 
            get => Product.GetAdditives().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList();
        }

        public ImmutableList<KeyValue> Alergens 
        { 
            get => Product.GetAlergens().Select(o => new KeyValue(o.Item1, o.Item2)).ToImmutableList();
        }

        public MainPageViewModel(Product product)
        {
            Product = product;
        }
    }

    public partial class MainPage : ContentPage
    {

        IPrinsService _service;

        public MainPage(IPrinsService service)
        {
            _service = service;
           
            Content = new ActivityIndicator() { IsRunning = true };

            Task.Run(() => LoadData())
                .ContinueWith(_ => Dispatcher.Dispatch(() => Build()));
        }

        async Task LoadData()
        {
            await Task.Delay(2000);
            try {
                BindingContext = new MainPageViewModel(await _service.GetByIdAsync(1));
            } catch (Exception ex) {
                Dispatcher.Dispatch(() => DisplayAlert("Error", ex.Message, "Cancel"));
            }
        }

        void Build()
        {
            InitializeComponent();
        }

    }
}