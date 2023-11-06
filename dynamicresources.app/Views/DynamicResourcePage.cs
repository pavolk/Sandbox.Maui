using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using dynamicresources.app.Resources.Strings;
using System.Globalization;

namespace dynamicresources.app.Views;

public partial class DynamicResourcePage : ContentPage
{
    private static readonly int RESOURCE_DEFAULT_VALUE = 20;

    private static readonly string RESOURCE_KEY = "LabelFontSize";

    private static readonly string LOCALE_KEY = "UserLocale";

    #region viewmodel

    [RelayCommand]
    private async Task ChangeLabelFontSize()
    {
        var popup = new ChangeFontSizeResourcePopup(Resources, RESOURCE_KEY);
        await this.ShowPopupAsync(popup);
    }

    private CultureInfo _locale;

    public CultureInfo SelectedLocale
    {
        get => _locale;
        set {
            if (_locale == value) return;
            _locale = value;
            Strings.Culture = value;
            Preferences.Default.Set(LOCALE_KEY, value.TwoLetterISOLanguageName);
            OnPropertyChanged();
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

    #endregion viewmodel

    public DynamicResourcePage()
    {
        // defining a Page-wide resource
        int value = Preferences.Default.Get(RESOURCE_KEY, RESOURCE_DEFAULT_VALUE);
        Resources.Add(RESOURCE_KEY, value);

        string loc = Preferences.Default.Get(LOCALE_KEY, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
        SelectedLocale = new CultureInfo(loc);

        Content = new VerticalStackLayout {
            Spacing = 10,
            Children = {
                new Label()
                    .BindText(() => Strings.DynamicResourcePage_Welcome)
                    .Center()
                    // (dynamic) binding the resource to a binding-property
                    .DynamicResource(Label.FontSizeProperty, RESOURCE_KEY)
                ,
                new Button()
                    .BindText(() => Strings.DynamicResourcePage_ToggleLocale)
                    .Center()
                    .Invoke(b => b.Clicked += (s,e) => {
                        var locale = SelectedLocale.TwoLetterISOLanguageName;
                        switch (locale) {
                            case "en": locale = "de"; break;
                            case "de": locale = "sk"; break;
                            case "sk": locale = "en"; break;
                            default:
                                locale = "en"; break;
                        }
                        SelectedLocale = new CultureInfo(locale);
                    })
                ,
                new Button()
                    .BindText(() => Strings.DynamicResourcePage_ToggleLightDarkTheme)
                    .Center()
                    .Invoke(b => b.Clicked += (s,a) => {
                        if (SelectedAppTheme == AppTheme.Dark) {
                            SelectedAppTheme = AppTheme.Light;
                        } else {
                            SelectedAppTheme = AppTheme.Dark;
                        }
                    })
                ,
                new Button()
                    .BindText(() => Strings.DynamicResourcePage_SetFontSize)
                    .Center()
                    // BUG: this will break the button's light/dark style switching...
                    //.BindCommand(nameof(ChangeLabelFontSizeCommand))
                    // ... and this works fine ?!?
                    .Invoke(b => b.Clicked += async (s, a) => {
                        var popup = new ChangeFontSizeResourcePopup(Resources, RESOURCE_KEY);
                        popup.Anchor = s as View;
                        await this.ShowPopupAsync(popup);
                        Preferences.Default.Set(RESOURCE_KEY, (int)Resources[RESOURCE_KEY]);
                    })
                ,
                new Button()
                    .BindText(() => Strings.DynamicResourcePage_ResetPreferences)
                    .Center()
                    .Invoke(b => b.Clicked += async (s, a) => {
                        bool _continue = await DisplayAlert(title: "",
                                    Strings.DynamicResourcePage_ResetPreferencesAlert,
                                    Strings.DynamicResourcePage_ContinueButtonLabel,
                                    Strings.CancelButtonLabel);
                        if (_continue) {
                            Preferences.Default.Clear();
                        }
                    })
            }
        };

        BindingContext = this;
    }
}