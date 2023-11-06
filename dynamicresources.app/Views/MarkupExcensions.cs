using CommunityToolkit.Maui.Markup;
using System.Globalization;

namespace dynamicresources.app.Views
{
    public static class MarkupExtensions

    {
        public static Picker ItemsSource<T>(this Picker itemsView, IEnumerable<T> itemsSource)
        {
            itemsView.ItemsSource = itemsSource?.ToList() ?? Enumerable.Empty<T>().ToList();
            itemsView.SelectedIndex = -1;
            return itemsView;
        }

        public static Button BindAction(this Button b, string commandPath, object? source = null, Func<string>? textGenerator = null)
        {
            if (textGenerator != null) {
                b.Bind(Button.TextProperty, "SelectedLocale", convert: (CultureInfo _) => textGenerator());
            }
            b.BindCommand(commandPath, source);
            return b;
        }

        public static Label BindText(this Label l, Func<string> textGenerator)
        {
            l.Bind(Label.TextProperty, "SelectedLocale", convert: (CultureInfo _) => textGenerator());
            return l;
        }

        // REVIEW: how to enforce the check that the viewmodel has "SelectedLocate" on compile-time

        public static Button BindText(this Button b, Func<string> textGenerator)
        {
            b.Bind(Button.TextProperty, "SelectedLocale", convert: (CultureInfo _) => textGenerator());
            return b;
        }
    }
}