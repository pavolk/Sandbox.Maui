using CommunityToolkit.Maui.Markup;
using Sharpnado.TaskLoaderView;
using Sharpnado.Tasks;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using Rows = CommunityToolkit.Maui.Markup.GridRowsColumns.Rows;

namespace TaskLoaderViewTestApp
{
    public class Game
    { }

    public class LocalizedException : Exception 
    {
        public LocalizedException(string message) : base(message) { }
    }

    public class TaskLoaderViewPage<TData> : ContentPage where TData : class //TViewModel :
    {
        enum RowSections { TopToolOrSnackbar, Body, BottomToolOrSnackbar }

        static readonly IValueConverter ErrorMessageConverter = new DefaultErrorMessageConverter();

        protected  ITaskLoaderViewModel<TData> ViewModel { get; }

        public TaskLoaderViewPage(ITaskLoaderViewModel<TData> vm)
        {
            ViewModel = vm;

            TaskMonitorConfiguration.ConsiderCanceledAsFaulted = true;

            var source = new RelativeBindingSource(RelativeBindingSourceMode.TemplatedParent);

            Content = new Grid() {
                RowDefinitions = Rows.Define(
                    (RowSections.TopToolOrSnackbar, Auto), 
                    (RowSections.Body, Star),
                    (RowSections.BottomToolOrSnackbar, Auto)
                )
                ,
                Children = {
                    new Grid() {
                        Children = {
                            /*
                            new Button()
                                 .Text("Refresh")
                                 .Bind(View.IsVisibleProperty, nameof(vm.Loader.IsCompleted), source: vm.Loader)
                                 .BindCommand(nameof(vm.RefreshOrReloadCommand))
                            ,
                            */
                            new Snackbar()
                                .BackgroundColor(Colors.Red)
                                .Bind(View.IsVisibleProperty, nameof(vm.Loader.ShowErrorNotification), source: vm.Loader, mode: BindingMode.TwoWay)
                                .Bind(Snackbar.TextProperty, nameof(vm.Loader.Error), source: vm.Loader, converter: ErrorMessageConverter)

                        }
                    }.Row(RowSections.TopToolOrSnackbar)
                    ,
                    new TemplatedTaskLoader() {
                        TaskLoaderNotifier = vm.Loader,
                        EmptyControlTemplate = new ControlTemplate(() =>
                            new ContentView() {
                                Content = new Label().Text("Empty").Center()
                            }
                            .Bind(BindableObject.BindingContextProperty, nameof(BindingContext), source: source)
                        )
                        ,
                        LoadingControlTemplate = new ControlTemplate(() =>
                            new VerticalStackLayout() {
                                Spacing = 10,
                                Children = {
                                    new ActivityIndicator() {
                                        IsRunning = true
                                    },
                                    new Label().Text("Loading..."),
                                    new Button().Text("Cancel")
                                        .BindCommand(nameof(vm.CancelCommand))
                                }
                            }.Center()
                             .Bind(BindableObject.BindingContextProperty, nameof(BindingContext), source: source)
                        )
                        ,
                        ResultControlTemplate = new ControlTemplate(() => 
                            Build()
                             .Bind(BindableObject.BindingContextProperty, nameof(BindingContext), source: source)
                        )
                        ,
                        ErrorControlTemplate = new ControlTemplate(() => 
                            new VerticalStackLayout() {
                                Spacing = 10,
                                Children = {
                                    new ActivityIndicator() { IsRunning = true },
                                    new Label().Bind("Loader.Error.Message", stringFormat: "Failed with \"{0}\"."),
                                }
                            }.Center()
                             .Bind(BindableObject.BindingContextProperty, nameof(BindingContext), source: source)
                        )
                    }.Row(RowSections.Body)
                }
            }.Padding(10);

            BindingContext = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnDisappearing();
        }

        protected virtual View Build()
        {
            return new Label().Text("Finished.").Center();
        }

    }
}
