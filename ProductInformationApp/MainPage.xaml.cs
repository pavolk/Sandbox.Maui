using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;
using ProductInformationApp.Services;
using System.Security.AccessControl;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

using MauiLayout = Microsoft.Maui.Controls.Compatibility.Layout;
using KeyValue = ProductInformationApp.Services.PrinsModelExtensions.KeyValue;

namespace ProductInformationApp;

public partial class MainPage : ContentPage
{
    public static Style<Label> ProductHeaderStyle { get; } = new (
        (Label.TextTransformProperty, TextTransform.Uppercase),
        (Label.FontAttributesProperty, FontAttributes.Bold),
        (Label.FontSizeProperty, 20)
    );

    public static Style<VerticalStackLayout> TextCellStyle { get; } = new (
       (VisualElement.MinimumWidthRequestProperty, 150),
       (VisualElement.MaximumWidthRequestProperty, 350),
       (MauiLayout.PaddingProperty, new Thickness(5)),
       (StackBase.SpacingProperty, 5)
    );

    public static Style<Label> HeaderLabelStyle { get; } = new (
       (Label.FontSizeProperty, 14),
       (Label.FontAttributesProperty, FontAttributes.Bold)
    );

    public static Style<Label> BodyLabelStyle { get; } = new Style<Label> (
       (Label.FontSizeProperty, 12),
       (Label.FontAttributesProperty, FontAttributes.None),
       (Label.LineBreakModeProperty, LineBreakMode.WordWrap),
       (Label.PaddingProperty, new Thickness(left: 5, top: 0, right: 5, bottom: 0))
    ).BasedOn(HeaderLabelStyle);

    public static Style<FlexLayout> FlexLayoutStyle { get; } = new(
        (FlexLayout.DirectionProperty, FlexDirection.Row),
        (FlexLayout.WrapProperty, FlexWrap.Wrap),
        (FlexLayout.JustifyContentProperty, FlexJustify.Start),
        (FlexLayout.AlignContentProperty, FlexAlignContent.Start),
        (FlexLayout.AlignItemsProperty, FlexAlignItems.Start)
    );

    class SectionHeader : ContentView 
    {
        public static Style<Label> Style { get; } = new(
            (Label.TextColorProperty, Colors.DodgerBlue),
            //(Label.FontAttributesProperty, FontAttributes.Bold),
            (Label.FontSizeProperty, 16)
        );

        public static Style<BoxView> UnderlineStyle { get; } = new(
            (BoxView.ColorProperty, Colors.LightGray),
            (BoxView.MaximumHeightRequestProperty, 1)
        );

        public SectionHeader(string title)
        {
            Content = new VerticalStackLayout() {
                Spacing = 2,
                Children = {
                            new Label().Text(title).Style(Style),
                            new BoxView().Style(UnderlineStyle)
                        }
            };
        }
    }

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

    private AppTheme _currentTheme = Application.Current.PlatformAppTheme;

    private AppTheme SelectedAppTheme
    {
        get => _currentTheme;
        set {
            if (_currentTheme == value) return;
            _currentTheme = value;
            Application.Current.UserAppTheme = value;
            OnPropertyChanged();
        }
    }

    void Build()
    {
        var vm = BindingContext as MainPageViewModel;

        var selector = new ProductInformationTemplateSelector();

        var brandUndManufacturerAid = new string[] { 
                vm.Product.Brand?.Value, 
                vm.Product.ManufacturerAid 
            }.Where(x => !string.IsNullOrEmpty(x));

        Content = new ScrollView() {
            Content = new VerticalStackLayout() { Spacing = 10, //new FlexLayout() { Direction = FlexDirection.Row, Wrap = FlexWrap.Wrap, 
                Children = {
                    new HorizontalStackLayout() {
                        Children = {
                            new Button().Text("Rebuild")
                                        .Invoke(b => b.Clicked += (_,_) => Build())
                            ,
                            new Button().Text("Dark/Light")
                                        .Invoke(b => b.Clicked += (_,_) => {
                                            if (SelectedAppTheme == AppTheme.Dark) {
                                                SelectedAppTheme = AppTheme.Light;
                                            } else {
                                                SelectedAppTheme = AppTheme.Dark;
                                            }
                                        })
                        }
                    }
                    ,
                    // Header
                    new VerticalStackLayout() {
                        Children = {
                            new Label().Text(vm.Product.Name)
                                       .Style(ProductHeaderStyle),
                            new Label().Text(vm.Product.Manufacturer?.ManufacturerShortName)
                                       .Style(BodyLabelStyle),
                            new Label().Invoke(l => l.Text = string.Join(" | ", brandUndManufacturerAid))
                                       .Style(BodyLabelStyle),
                        }
                    }
                    ,
                    new SectionHeader("Common Information/Allgemeine Informationen")
                    ,
                    new FlexLayout()
                        .ItemsSource (vm.Product.ProductCertifications)
                        .ItemTemplateSelector(selector)
                        .Style(FlexLayoutStyle)
                    ,
                    new CollectionView()
                        .ItemsSource (vm.Product.GetCommonInformation()
                            .Select((kv, i) => {
                                return new KeyValue(kv.Key, kv.Value, i, Columns.Define(150, Star));
                            })
                         )
                        .ItemTemplate(selector)
                    ,
                    new SectionHeader("Hints/Allgemeine Hinweise")
                    ,
                    new FlexLayout()
                        .ItemsSource (vm.Product.GetHints())
                        .ItemTemplateSelector(selector)
                        .Style(FlexLayoutStyle)
                    ,
                    new SectionHeader("Packaging/Packungseinheiten")
                    ,
                    new FlexLayout()
                        .ItemsSource (vm.Product.GetPackaging())
                        .ItemTemplateSelector(selector)
                        .Style(FlexLayoutStyle)
                    ,
                }
            }.Padding(5)
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(() => LoadData())
            .ContinueWith(_ => Dispatcher.Dispatch(() => Build()));
    }

}