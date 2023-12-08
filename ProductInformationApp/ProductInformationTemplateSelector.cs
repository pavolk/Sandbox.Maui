using CommunityToolkit.Maui.Markup;
using ProductInformationApp.Services;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace ProductInformationApp;

using KeyValue = PrinsModelExtensions.KeyValue;

public class ProductInformationTemplateSelector : DataTemplateSelector
{
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is Prins.Product product) {
            return new DataTemplate(() => new Label().Bind(nameof(product.Name)));
        }

        if (item is Prins.UserSpecificInfo ui) {
            return new DataTemplate(() => new VerticalStackLayout() {
                    Children = {
                        new Label().Text(ui.Header).Style(MainPage.HeaderLabelStyle),
                        new Label().Text(ui.Text).Style(MainPage.BodyLabelStyle)
                    }
                }.Style(MainPage.TextCellStyle)
            );
        }

        if (item is Prins.StorageInstruction si) {
            return new DataTemplate(() => new VerticalStackLayout() {
                Children = {
                        new Label().Text("Storage Information/Aufbewahrungshinweise").Style(MainPage.HeaderLabelStyle),
                        new Label().Text(si.Value).Style(MainPage.BodyLabelStyle)
                    }
                }.Style(MainPage.TextCellStyle)
            );
        }

        if (item is Prins.PreparationInfo pi) {
            return new DataTemplate(() => new VerticalStackLayout() {
                MinimumWidthRequest = 300,
                Children = {
                        new Label().Text("Preparation Information/Zubereitungsempfehlung").Style(MainPage.HeaderLabelStyle),
                        new Label().Text(pi.Value).Style(MainPage.BodyLabelStyle)
                    }
                }.Style(MainPage.TextCellStyle) //.Add((VisualElement.MinimumWidthRequestProperty, 300)))
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
                                     .Style(MainPage.BodyLabelStyle)
            });
        }

        if (item is IEnumerable<Prins.DietType> dt) {
            var types = string.Join(", ", dt.Select(dt => dt.DietTypeProperty.Text[0]));
            return new DataTemplate(() => new VerticalStackLayout() {
                Children = {
                        new Label().Text("Diet types/Kostformen")
                            .Style(MainPage.HeaderLabelStyle),
                        new Label().Text(types)
                            .Style(MainPage.BodyLabelStyle)
                    }
                }.Style(MainPage.TextCellStyle)
            );
        }

        if (item is Prins.TradingUnit tu) {
            var fields = new KeyValue[] {
                new (nameof(tu.Type), tu.Type),
                new (nameof(tu.DrainWeight), tu.DrainWeight),
                new (nameof(tu.GrossWeight), tu.GrossWeight),
                new (nameof(tu.NetWeight), tu.NetWeight),
                new (nameof(tu.Dimension), tu.Dimension)
            }.Select((kv, i) => new KeyValue(kv.Key, kv.Value, i));
            return new DataTemplate(() => new VerticalStackLayout() {
                Children = {
                        new Label().Text("Trading unit/Verkaufseinheit").Style(MainPage.HeaderLabelStyle),
                        new CollectionView()
                            .ItemsSource(fields)
                            .ItemTemplate(this)
                    }
                }.Style(MainPage.TextCellStyle)
            );
        }

        if (item is Prins.UnitPack up) {
            var fields = new List<PrinsModelExtensions.KeyValue>() {
                new (nameof(up.ContentInfo), up.ContentInfo),
                new (nameof(up.GrossWeight), up.GrossWeight),
                new (nameof(up.NetWeight), up.NetWeight),
                new (nameof(up.DrainWeight), up.DrainWeight),
                new (nameof(up.Dimension), up.Dimension),
            }.Select((kv, i) => new KeyValue(kv.Key, kv.Value, i));
            return new DataTemplate(() => new VerticalStackLayout() {
                Children = {
                        new Label().Text("Unit pack/Einzelverpackung/-einheit").Style(MainPage.HeaderLabelStyle),
                        new CollectionView()
                            .ItemsSource(fields)
                            .ItemTemplate(this)
                    }
                }.Style(MainPage.TextCellStyle)
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
            }.Select((kv, i) => new KeyValue(kv.Key, kv.Value, i));
            return new DataTemplate(() => new VerticalStackLayout() {
                Children = {
                        new Label().Text("Palette").Style(MainPage.HeaderLabelStyle),
                        new CollectionView()
                            .ItemsSource(fields)
                            .ItemTemplate(this)
                    }
                }.Style(MainPage.TextCellStyle)
            );
        }

        if (item is KeyValue kv) {

            var EvenRowStyle = new Style<Label>()
                    .BasedOn(MainPage.BodyLabelStyle)
                    .AddAppThemeBindings(
                        (Label.BackgroundColorProperty, Colors.White, Colors.Black),
                        (Label.TextColorProperty, Colors.Black, Colors.White)
                    );

            var OddRowStyle = new Style<Label>()
                    .BasedOn(MainPage.BodyLabelStyle)
                    .AddAppThemeBindings(
                        (Label.BackgroundColorProperty, Colors.White.WithLuminosity(0.9f), 
                                                        Colors.Black.WithLuminosity(0.2f)),
                        (Label.TextColorProperty, Colors.Black, Colors.White)
                    );

            var style = kv.RowIndex % 2 == 0 ? EvenRowStyle : OddRowStyle;

            var columns = kv.columns ?? Columns.Define(150, 100);

            return new DataTemplate(() => new Grid() {
                ColumnDefinitions = columns,
                Children = {
                    new Label().Column(0)
                               .Text(kv.Key)
                               .Style(style)
                               .Fill().TextStart()
                               ,
                    new Label().Column(1)
                               .Text(kv.Value.ToString())
                               .Style(style)
                               .Fill().TextStart()
                },
            });
        }

        return new DataTemplate(() => new Label().Text($"Unknown data-type {item.GetType()}"));
    }
}
