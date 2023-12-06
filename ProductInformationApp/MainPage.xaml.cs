using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;
using ProductInformationApp.Services;
using System.Collections;

namespace ProductInformationApp;

public class ProductInformationTemplateSelector : DataTemplateSelector
{
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var UserSpecificInfo_MinMaxWidthRequest = (300, 500);

        if (item is Prins.Product product) {
            return new DataTemplate(() => new Label().Bind(nameof(product.Name)));
        }

        if (item is Prins.UserSpecificInfo ui) {
            return new DataTemplate(() => new Frame() {
                MinimumWidthRequest = UserSpecificInfo_MinMaxWidthRequest.Item1,
                MaximumWidthRequest = UserSpecificInfo_MinMaxWidthRequest.Item2,
                CornerRadius = 25,
                Content = new VerticalStackLayout() {
                    Spacing = 5,
                    Children = {
                        new Label().Text(ui.Header).Bold(),
                        new Label() { LineBreakMode = LineBreakMode.WordWrap }.Text(ui.Text)
                    }
                } 
            });
        }

        if (item is Prins.ProductCertification pc) {
            return new DataTemplate(() => new Frame() {
                CornerRadius = 25,
                HeightRequest = 50,
                WidthRequest = 150,
                Content = new Label().Text(pc.Text)
                                     .FontSize(10)
                                     .Center()
                                     .TextCenter()
            });
        }

        if (item is Prins.StorageInstruction si) {
            return new DataTemplate(() => new Frame() {
                        CornerRadius = 25,
                        Content = new VerticalStackLayout() { 
                            Spacing = 5,
                            Children= {
                                new Label().Text("Storage Information/Aufbewahrungshinweise:").Bold(),
                                new Label() { LineBreakMode = LineBreakMode.WordWrap }.Text(si.Value),
                            }
                        
                        }
                    });
        }

        if (item is Prins.PreparationInfo pi) {
            return new DataTemplate(() => new Frame() {
                CornerRadius = 25,
                Content = new VerticalStackLayout() {
                    Spacing = 5,
                    Children = {
                        new Label().Text("Preparation Information/Zubereitungsempfehlung:").Bold(),
                        new Label() { LineBreakMode = LineBreakMode.WordWrap }.Text(pi.Value)
                    }
                }
            });
        }

        if (item is Prins.DietType dt) {
            return new DataTemplate(() => new Frame() {
                CornerRadius = 25,
                HeightRequest = 50,
                WidthRequest = 150,
                Content = new Label().Text(dt.Text[0]).FontSize(10).Center().TextCenter()
            });
        }

        if (item is Prins.TradingUnit tu) {
            var fields = new List<PrinsModelExtensions.KeyValue>() {
                new (nameof(tu.GrossWeight), tu.GrossWeight),
                new (nameof(tu.NetWeight), tu.NetWeight),
            };
            return new DataTemplate(() => new Frame() {
                Content = new VerticalStackLayout() {
                    Spacing = 5,
                    Children = {
                        new Label().Text("Trading unit/Verkaufseinheit").Bold(),
                        new CollectionView()
                            .ItemsSource(fields)
                            .ItemTemplate(this)
                    }
                }
            });
        }

        if (item is Prins.UnitPack up) {
            var fields = new List<PrinsModelExtensions.KeyValue>() {
                new (nameof(up.Dimension), up.Dimension),
                new (nameof(up.DrainWeight), up.DrainWeight)
            };
            return new DataTemplate(() => new Frame() {
                Content = new VerticalStackLayout() {
                    Spacing = 5,
                    Children = {
                        new Label().Text("Unit pack/Einzelverpackung/-einheit").Bold(),
                        new CollectionView()
                            .ItemsSource(fields)
                            .ItemTemplate(this)
                    }
                }
            });
        }

        if (item is Prins.Palette plt) {
            var fields = new List<PrinsModelExtensions.KeyValue>() {
                new (nameof(plt.Dimension), plt.Dimension),
                new (nameof(plt.TotalWeightKg), plt.TotalWeightKg)
            };
            return new DataTemplate(() => new Frame() { 
                Content = new VerticalStackLayout() {
                    Spacing = 5,
                    Children = {
                        new Label().Text("Palette").Bold(),
                        new CollectionView()
                            .ItemsSource(fields)
                            .ItemTemplate(this)
                    }
                }
            });
        }

        if (item is PrinsModelExtensions.KeyValue kv) {
            return new DataTemplate(() => new HorizontalStackLayout() {
                Spacing = 5,
                Children = {
                    new Label().Text(kv.Key).Bold(), 
                    new Label().Text(kv.Value.ToString())
                }
            });
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
    }

    async Task LoadData()
    {
        //await Task.Delay(2000);
        try {
            BindingContext = new MainPageViewModel(await _service.GetByIdAsync(1));
        } catch (Exception ex) {
            Dispatcher.Dispatch(() => DisplayAlert("Error", ex.Message, "Cancel"));
        }
    }

    void Build()
    {
        var sectionHeaderStyle = new Style<Label>(
            (Label.TextColorProperty, Colors.Black),
            (Label.BackgroundColorProperty, Colors.LightGray),
            (Label.FontAttributesProperty, FontAttributes.Bold),
            (Label.FontSizeProperty, 16)
        );

        var flexLayoutStyle = new Style<FlexLayout>(
            (FlexLayout.DirectionProperty, FlexDirection.Row),
            (FlexLayout.WrapProperty, FlexWrap.Wrap),
            (FlexLayout.JustifyContentProperty, FlexJustify.Start),
            (FlexLayout.AlignContentProperty, FlexAlignContent.Start),
            (FlexLayout.AlignItemsProperty, FlexAlignItems.Start)
        );

        var vm = BindingContext as MainPageViewModel;

        var selector = new ProductInformationTemplateSelector();

        Content = new ScrollView() {
            Content = new VerticalStackLayout() { Spacing = 10, //new FlexLayout() { Direction = FlexDirection.Row, Wrap = FlexWrap.Wrap, 
                Children = {
                    new Button().Text("Rebuild")
                                .Invoke(b => b.Clicked += (b,e) => Build())
                                .Basis(new FlexBasis(1, true))
                    ,
                    new CollectionView()
                        .ItemsSource(vm.Product.GetOverview())
                        .ItemTemplate(selector)
                    ,
                    new Label().Text("Common Information/Allgemeine Informationen:")
                               .Style(sectionHeaderStyle)
                               .Basis(new FlexBasis(1, true))
                    ,
                    new CollectionView()
                        .ItemsSource (vm.Product.GetCommonInformation())
                        .ItemTemplate(selector)
                    ,
                    new Label().Text("Hints/Allgemeine Hinweise:")
                               .Style(sectionHeaderStyle)
                               .Basis(new FlexBasis(1, true))
                    ,
                    new FlexLayout()
                        .ItemsSource (vm.Product.UserSpecificInfos)
                        .ItemTemplateSelector(selector)
                        .Style(flexLayoutStyle)
                    ,
                    new FlexLayout()
                        .ItemsSource (vm.Product.ProductCertifications)
                        .ItemTemplateSelector(selector)
                        .Style(flexLayoutStyle)
                    ,
                    new Label().Text("Packaging/Packungseinheiten:")
                               .Style(sectionHeaderStyle)
                               .Basis(new FlexBasis(1, true))
                    ,
                    new FlexLayout()
                        .ItemsSource (vm.Product.GetPackaging())
                        .ItemTemplateSelector(selector)
                        .Style(flexLayoutStyle)
                    ,
                }
            }
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(() => LoadData())
            .ContinueWith(_ => Dispatcher.Dispatch(() => Build()));
    }

}