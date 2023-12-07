using CommunityToolkit.Maui.Markup;

namespace ProductInformationApp;

public class CarouselViewPage : ContentPage
{
    class GeneralInformationAndPackaging
    {
    }

    class NutrientsAdditivesAllergens
    {
    }

    class TemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is GeneralInformationAndPackaging gi) {
                return new DataTemplate(() => new Label().Text(nameof(GeneralInformationAndPackaging))
                            .TextCenter().Center().Fill());
            }

            if (item is NutrientsAdditivesAllergens naa) {
                return new DataTemplate(() => new Label().Text(nameof(NutrientsAdditivesAllergens))
                            .TextCenter().Center().Fill());
            }

            return new DataTemplate(() => new Label().Text("Unknown item type!")
                            .TextCenter().Center().Fill());
        }
    }

    IList<object> _items = new List<object>() {
            new GeneralInformationAndPackaging(),
            new NutrientsAdditivesAllergens(),
            "Placeholder"
        };

    public IList<object> Items { get => _items; }

    public CarouselViewPage()
    {
        var selector = new TemplateSelector();

        var indicator = new IndicatorView().Center();

        Content = new VerticalStackLayout() {
            Children = { 
                new CarouselView() { 
                    IndicatorView = indicator 
                }
                .Bind(nameof(Items))
                .ItemTemplate(selector)
                ,
                indicator
            }
        }.Fill();

        BindingContext = this;
	}
}