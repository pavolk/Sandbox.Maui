using CommunityToolkit.Maui.Markup;
using ProductInformationApp.Services;
using System.Collections;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace ProductInformationApp;

public class ProductInformationTemplateSelector : DataTemplateSelector
{
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is Prins.Product product) {

            return new DataTemplate(() => new Label().Bind(nameof(product.Name)));

        }

        if (item is Prins.ProductCertifications pcs) {
            return new DataTemplate(() => new HorizontalStackLayout()
                                                .ItemsSource(pcs.ProductCertification)
                                                .ItemTemplate(this));
        }

        if (item is MainPageViewModel.ProductInfoChunk pic) {
            return new DataTemplate(() =>
                new Grid() {
                    RowDefinitions = Rows.Define(Auto, Auto),
                    RowSpacing = 5,
                    Children = {
                        new Label()
                            .Row(0)
                            .Text(pic.Header)
                            .Bold().BackgroundColor(Colors.LightGray)
                        ,
                        new Grid() {
                            Children = {
                                new CollectionView()
                                    .ItemsSource(pic.Data)
                                    .ItemTemplate(this)
                            }
                        }.Row(1)
                         .Paddings(left:5, right: 5)
                    }
                });
        }

        if (item is MainPageViewModel.TextCell tc) {
            return new DataTemplate(() =>
                new Grid() {
                    RowDefinitions = Rows.Define(Auto, Star),
                    Children = {
                        new Label() { LineBreakMode = LineBreakMode.HeadTruncation }
                            .Row(0)
                            .Text(tc.Title).Bold()
                        ,
                        new Grid() {
                            Children = {
                                new CollectionView()
                                    .ItemsSource(tc.Body)
                                    .ItemTemplate(this)
                            }
                        }.Row(1)
                         .Paddings(left:5, right: 5)
                    }
                });
        }

        if (item is MainPageViewModel.KeyValue kv) {
            return new DataTemplate(() =>
                new Grid() {
                    ColumnDefinitions = Columns.Define(250, Auto),
                    Children = {
                        new Label() { LineBreakMode = LineBreakMode.HeadTruncation }
                            .Column(0)
                            .Text(kv.Key).Bold()
                        ,
                        new Label() { LineBreakMode = LineBreakMode.WordWrap }
                            .Column(1)
                            .Text(kv.Value.ToString())
                    }
                });
        }

        if (item is MainPageViewModel.KeyValueUnit kvu) {
            return new DataTemplate(() =>
                new Grid() {
                    ColumnDefinitions = Columns.Define(250, Auto, Auto),
                    Children = {
                        new Label() { LineBreakMode = LineBreakMode.HeadTruncation }
                            .Column(0)
                            .Text(kvu.Key)
                            .Bold()
                        ,
                        new Label()
                            .Column(1)
                            .Text(kvu.Value.ToString())
                        ,
                        new Label()
                            .Column(2)
                            .Text(kvu.Unit)
                    }
                });
        }

        if (item is Prins.ProductCertification pc) {
            return new DataTemplate(() => {
                return new Label().Text(pc.Text);
            });
        }

        if (item is string) {
            return new DataTemplate(() => new Label().Text(item.ToString()));
        }

        if (item is IEnumerable items) {
            return new DataTemplate(() => new CollectionView()
                                                .ItemsSource(items)
                                                .ItemTemplate(this));
        }

        return new DataTemplate(() => new Label().Text($"Unknown data-type {item.GetType()}"));
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
        //InitializeComponent();

        var vm = BindingContext as MainPageViewModel;

        var selector = new ProductInformationTemplateSelector();

        Content = new ScrollView() {
            Content = new CollectionView()
                        .ItemsSource(vm.Chunks ?? Enumerable.Empty<string>())
                        .ItemTemplate(selector)
        };
    }

}