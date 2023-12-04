
using CommunityToolkit.Maui.Markup;
using ProductInformationApp.Services;
using Prins;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

using Microsoft.Maui.Layouts;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace ProductInformationApp;

public class HorizontalLine : BoxView 
{
    public HorizontalLine() 
    {
        HeightRequest = 1;
        BackgroundColor = Colors.LightGray;
    }
}

public class ParagraphView : ContentView
{
    public string Title { get; set; }  = string.Empty;
    public string Body { get; set; } = string.Empty;

    public ParagraphView(string title, string body)
    {
        Title = title;
        Body = body;
        Content = new VerticalStackLayout() {
            Spacing = 5,
            Children = {
                new Label().Text(Title).Bold(),
                new Label().Text(Body)
            }
        };
    }
}

public partial class ProductInformationViewModel : ObservableObject 
{
    IPrinsService _service;

    [ObservableProperty]
    Product product;

    public ProductInformationViewModel(IPrinsService service)
    { 
        _service = service;
    }

    public async Task LoadData(int id)
    {
        try {
            await Task.Delay(2000);
            Product = await _service.GetByIdAsync(id);
        } catch (Exception ex) { 
            // log.Error(ex)
            // DisplayAlert()
            Debug.WriteLine(ex);
        }
    }
}

public class SimpleProductInformationPage : ContentPage
{
    public SimpleProductInformationPage(ProductInformationViewModel vm)
    {
        BindingContext = vm;

        Content = new ActivityIndicator() { IsRunning = true };

        Task.Run(() => vm.LoadData(1))
            .ContinueWith(_ => Dispatcher.Dispatch(() => Build()));
    }

    async Task Build()
    {
        const int NutrientLabel_MinWidth = 250;
        const int AllergentShortName_MinWidth = 250;
        const int AllergentText_MinWidth = 30;

        var vm = BindingContext as ProductInformationViewModel;
        if (vm == null) {
            await DisplayAlert("Error", "Wrong binding context", "Cancel");
            return;
        }

        var p = vm.Product;

        Content = new ScrollView() {
            Content = new VerticalStackLayout() {
                Children = {

                    new FlexLayout() {
                        Direction = FlexDirection.Row,
                        Wrap = FlexWrap.Wrap,
                        Children = {
                            new ParagraphView(nameof(p.StorageInstruction), p.StorageInstruction.Value)
                            ,
                            new ParagraphView(nameof(p.CommonInfo), p.CommonInfo.Value)
                            ,
                            new ParagraphView(nameof(p.PreparationInfo), p.PreparationInfo.Value)
                            ,
                            new HorizontalStackLayout() { Spacing = 10 }
                                .BackgroundColor(Colors.LightCoral)
                                .ItemsSource(p?.AdditionalProductInfos)
                                .ItemTemplate(new DataTemplate(() =>
                                    new ParagraphView(nameof(p.CommonInfo), p.CommonInfo.Value)
                                ))
                            ,
                            new HorizontalStackLayout() { Spacing = 10 }
                                .BackgroundColor(Colors.LightBlue)
                                .ItemsSource(p?.ProductCertifications)
                                .ItemTemplate(new DataTemplate(() => new Label().Bind(nameof(ProductCertification.Text))))
                            ,
                            new HorizontalStackLayout() { Spacing = 10 }
                                .BackgroundColor(Colors.LightCyan)
                                .ItemsSource(p?.DietTypes)
                                .ItemTemplate(new DataTemplate(() => new Label().Bind(nameof(DietType))))
                        }
                    }
                    ,
                    new Label().Text(nameof(p.Nutrients)),
                    new HorizontalLine()
                    ,
                    new VerticalStackLayout() { Spacing = 5 }
                        .BackgroundColor(Colors.LightGreen)
                        .ItemsSource(p?.Nutrients?.Nutrient ?? Enumerable.Empty<Nutrient>())
                        .ItemTemplate(new DataTemplate(() =>
                             new Grid() {
                                ColumnSpacing = 1,
                                ColumnDefinitions = Columns.Define(Auto, Auto, Auto),
                                Children = {
                                    new Label().Column(0).Bind(nameof(Nutrient.Name)).MinWidth(NutrientLabel_MinWidth),
                                    new Label().Column(1).Bind(nameof(Nutrient.Value)),
                                    new Label().Column(2).Bind(nameof(Nutrient.Unit))
                                }
                            }
                        ))
                    ,
                    new Label().Text(nameof(p.Allergens)),
                    new HorizontalLine()
                    ,
                    //new VerticalStackLayout() { Spacing = 5 }

                    new FlexLayout() {
                        Direction = FlexDirection.Row,
                        Wrap = FlexWrap.Wrap,
                    }.BackgroundColor(Colors.LightSalmon)
                     .ItemsSource(p?.Allergens ?? Enumerable.Empty<Allergen>())
                     .ItemTemplate(new DataTemplate(() =>
                        new Grid() {
                            ColumnSpacing = 5,
                            ColumnDefinitions = Columns.Define(Auto, Auto, Auto),
                            Children = {
                                new Label().Column(0).Bind(nameof(Allergen.ShortName)).MinWidth(AllergentShortName_MinWidth),
                                new Label().Column(1).Bind(nameof(Allergen.Text)).MinWidth(AllergentText_MinWidth),
                            }
                        }
                    ))
                }
            }
        };
    }
}