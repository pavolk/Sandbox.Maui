using Microsoft.Maui.Layouts;
using CommunityToolkit.Maui.Markup;
using ProductInformationApp.Services;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace ProductInformationApp;

public class ProductInformationTemplateSelector : DataTemplateSelector
{
    static Style<Label> HeaderLabelStyle { get; } = new Style<Label>(
       (Label.FontSizeProperty, 14),
       (Label.FontAttributesProperty, FontAttributes.Bold)
    );

    static Style<Label> BodyLabelStyle = new Style<Label>(
       (Label.FontSizeProperty, 12),
       (Label.FontAttributesProperty, FontAttributes.None),
       (Label.LineBreakModeProperty, LineBreakMode.WordWrap),
       (Label.PaddingProperty, new Thickness(left: 5, top: 0, right: 5, bottom: 0))
    ).BasedOn(HeaderLabelStyle);

    static Style<VerticalStackLayout> TextCellStyle = new Style<VerticalStackLayout>(
       (VisualElement.MinimumWidthRequestProperty, 150),
       (VisualElement.MaximumWidthRequestProperty, 350),
       (Layout.PaddingProperty, new Thickness(5)),
       (StackBase.SpacingProperty, 5)
    );

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is Prins.Product product) {
            return new DataTemplate(() => new Label().Bind(nameof(product.Name)));
        }

        if (item is Prins.UserSpecificInfo ui) {
            return new DataTemplate(() => new VerticalStackLayout() {
                    Children = {
                        new Label().Text(ui.Header).Style(HeaderLabelStyle),
                        new Label().Text(ui.Text).Style(BodyLabelStyle)
                    }
                }.Style(TextCellStyle)
            );
        }

        if (item is Prins.StorageInstruction si) {
            return new DataTemplate(() => new VerticalStackLayout() {
                Children = {
                        new Label().Text("Storage Information/Aufbewahrungshinweise").Style(HeaderLabelStyle),
                        new Label().Text(si.Value).Style(BodyLabelStyle)
                    }
                }.Style(TextCellStyle)
            );
        }

        if (item is Prins.PreparationInfo pi) {
            return new DataTemplate(() => new VerticalStackLayout() {
                MinimumWidthRequest = 300,
                Children = {
                        new Label().Text("Preparation Information/Zubereitungsempfehlung").Style(HeaderLabelStyle),
                        new Label().Text(pi.Value).Style(BodyLabelStyle)
                    }
                }.Style(TextCellStyle) //.Add((VisualElement.MinimumWidthRequestProperty, 300)))
            );
        }

        if (item is Prins.ProductCertification pc) {
            return new DataTemplate(() => new Frame() {
                CornerRadius = 25,
                HeightRequest = 50,
                WidthRequest = 150,
                Content = new Label().Text(pc.Text)
                                     .Center()
                                     .TextCenter()
                                     .Style(BodyLabelStyle)
            });
        }

        if (item is IEnumerable<Prins.DietType> dt) {
            var types = string.Join(", ", dt.Select(dt => dt.DietTypeProperty.Text[0]));
            return new DataTemplate(() => new VerticalStackLayout() {
                Children = {
                        new Label().Text("Diet types/Kostformen").Style(HeaderLabelStyle),
                        new Label().Text(types).Style(BodyLabelStyle)
                    }
                }.Style(TextCellStyle)
            );
        }

        if (item is Prins.TradingUnit tu) {
            var fields = new List<PrinsModelExtensions.KeyValue>() {
                new (nameof(tu.Type), tu.Type),
                new (nameof(tu.DrainWeight), tu.DrainWeight),
                new (nameof(tu.GrossWeight), tu.GrossWeight),
                new (nameof(tu.NetWeight), tu.NetWeight),
                new (nameof(tu.Dimension), tu.Dimension)
            };
            return new DataTemplate(() => new VerticalStackLayout() {
                Children = {
                        new Label().Text("Trading unit/Verkaufseinheit").Style(HeaderLabelStyle),
                        new CollectionView()
                            .ItemsSource(fields)
                            .ItemTemplate(this)
                    }
                }.Style(TextCellStyle)
            );
        }

        if (item is Prins.UnitPack up) {
            var fields = new List<PrinsModelExtensions.KeyValue>() {
                new (nameof(up.ContentInfo), up.ContentInfo),
                new (nameof(up.GrossWeight), up.GrossWeight),
                new (nameof(up.NetWeight), up.NetWeight),
                new (nameof(up.DrainWeight), up.DrainWeight),
                new (nameof(up.Dimension), up.Dimension),
            };
            return new DataTemplate(() => new VerticalStackLayout() {
                Children = {
                        new Label().Text("Unit pack/Einzelverpackung/-einheit").Style(HeaderLabelStyle),
                        new CollectionView()
                            .ItemsSource(fields)
                            .ItemTemplate(this)
                    }
                }.Style(TextCellStyle)
            );
        }

        if (item is Prins.Palette plt) {
            var fields = new List<PrinsModelExtensions.KeyValue>() {
                new (nameof(plt.Type), plt.Type),
                new (nameof(plt.Handling), plt.Handling),
                new (nameof(plt.UnitsPerPalette), plt.UnitsPerPalette),
                new (nameof(plt.UnitsPerTier), plt.UnitsPerTier),
                new (nameof(plt.Dimension), plt.Dimension),
                new (nameof(plt.TotalWeightKg), plt.TotalWeightKg)
            };
            return new DataTemplate(() => new VerticalStackLayout() {
                Children = {
                        new Label().Text("Palette").Style(HeaderLabelStyle),
                        new CollectionView()
                            .ItemsSource(fields)
                            .ItemTemplate(this)
                    }
                }.Style(TextCellStyle)
            );
        }

        if (item is PrinsModelExtensions.KeyValue kv) {
            return new DataTemplate(() => new Grid() {
                ColumnSpacing = 5,
                ColumnDefinitions = Columns.Define(150, 300),
                Children = {
                    new Label().Column(0).Text(kv.Key).Style(BodyLabelStyle),
                    new Label().Column(1).Text(kv.Value.ToString()).Style(BodyLabelStyle),
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
                    new FlexLayout()
                        .ItemsSource (vm.Product.ProductCertifications)
                        .ItemTemplateSelector(selector)
                        .Style(flexLayoutStyle)
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
                        .ItemsSource (vm.Product.GetHints())
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