using ProductInformationApp.Services;

namespace ProductInformationApp
{
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