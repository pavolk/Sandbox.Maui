using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Sharpnado.TaskLoaderView;
using System.Diagnostics;

namespace TaskLoaderViewTestApp
{
    public class BaseViewModel : ObservableObject
    {
        // TODO: this will be remoted once the services are there...
        private ContentPage _page;


        public virtual void OnAppearing(ContentPage page)
        {
            _page = page;
        }

        public virtual void OnDisappearing(ContentPage page)
        { 
            _page = null;
        }

        protected Task<string> DisplayYesNoCancelPopup(string message, string yes, string no, string cancel)
        {
            return _page.DisplayActionSheet(title: message, cancel: cancel, destruction: null, buttons: new string[] { yes, no });
        }

    }


    public class BasePage : ContentPage
    {
        BaseViewModel _vm;

        protected BasePage(BaseViewModel vm)
        {
            _vm = vm;
            Content = Build();

        }

        protected virtual View Build()
        {
            return new VerticalStackLayout() {
                Spacing = 10,
                Children = {
                    new ActivityIndicator() { IsRunning = true },
                    new Label().Text("Loading...")
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm?.OnAppearing(this);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _vm?.OnDisappearing(this);
        }
    }



    public partial class BaseAsyncViewModel : BaseViewModel
    {
        public CompositeTaskLoaderNotifier Loader { get; }

        public TaskLoaderCommand LoadCommand { get; }

        public TaskLoaderCommand SaveCommand { get; }

        CancellationTokenSource _cancelationSource;

        public BaseAsyncViewModel()
        {
            _cancelationSource = new CancellationTokenSource();

            LoadCommand = new TaskLoaderCommand(() => LoadDataAsync(false));

            LoadCommand.Notifier.PropertyChanged += (s, e) => {
                Debug.WriteLine("LoadCommand.Notifier:" + e.PropertyName);
            };

            SaveCommand = new TaskLoaderCommand(() => SaveDataAsync());

            SaveCommand.Notifier.PropertyChanged += (s, e) => {
                Debug.WriteLine("SaveCommand.Notifier:" + e.PropertyName);
            };

            Loader = CompositeTaskLoaderNotifier
                            .ForCommands()
                            .WithCommands(LoadCommand, SaveCommand)
                            .Build();

            Loader.PropertyChanged += (s, e) => {
                Debug.WriteLine("CompositeTaskLoaderNotifier:" + e.PropertyName);
            };

        }

        [RelayCommand]
        private void Cancel()
        {
            _cancelationSource.Cancel();
        }

        public async Task LoadDataAsync(bool isRefreshing)
        {
            Debug.WriteLine("LoadDataAsync");
            await Task.Delay(TimeSpan.FromSeconds(2));
        }

        public async Task SaveDataAsync()
        {
            Debug.WriteLine("SaveDataAsync");
            await Task.Delay(TimeSpan.FromSeconds(2));
        }

        public override void OnAppearing(ContentPage page)
        {
            base.OnAppearing(page);
            LoadCommand.Execute(this);
        }

        public override async void OnDisappearing(ContentPage page)
        {

            // SaveCommand.Execute(this);
            var result = await DisplayYesNoCancelPopup("Save changes?", "Save Changes", "Continue", "Cancel");
            Debug.WriteLine($"{result}");

            base.OnDisappearing(page);

        }

    }


    public class BaseAsyncPage : BasePage
    {
        public BaseAsyncPage(BaseAsyncViewModel vm) : base(vm)
        {
            Content = new TemplatedTaskLoader() {

                ErrorControlTemplate = new ControlTemplate(() => new Label().Text("Failed.").Center())
                ,
                LoadingControlTemplate = new ControlTemplate(() => new Label().Text("Loading...").Center())
                ,
                ResultControlTemplate = new ControlTemplate(() => Build())

            }.Bind(TemplatedTaskLoader.TaskLoaderNotifierProperty, nameof(vm.Loader));

            BindingContext = vm;
        }

        protected override View Build()
        {
            return new Label().Text("Finished loading.").Center();
        }

    }

}
