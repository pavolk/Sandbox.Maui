
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using dynamicresources.app.Resources.Strings;
using System.Globalization;

namespace dynamicresources.app.Views;

partial class ChangeFontSizeResourcePopup : Popup 
{
	ResourceDictionary _res;

	string _key = string.Empty;

	int _savedOriginalValue;

	int _fontSize;

    public int FontSize 
	{ 
		get => _fontSize; 
		set { 
			if (_fontSize != value) {
				_fontSize = value;
                _res[_key] = _fontSize;
                OnPropertyChanged();
				ApplyResourceChangeCommand.NotifyCanExecuteChanged();
            }
        } 
	}

	public ChangeFontSizeResourcePopup(ResourceDictionary res, string key)
	{
		_res = res;
		_key = key;

        if (!res.ContainsKey(_key)){
			Content = new Label().Text("Wrong resource dictionary...").Center();
			return;
		}

		_savedOriginalValue = (int)res["LabelFontSize"];
		_fontSize = _savedOriginalValue;

        Content = new VerticalStackLayout() {
			Spacing = 5,
			Children = {
				new Label() { 
						LineBreakMode = LineBreakMode.WordWrap
					}
					.Bind(nameof(FontSize), stringFormat: Strings.FontSizePopup_ActualFontSizeText)
				,
				new Button()
					.Text("+")
					.Invoke(b => b.Clicked += (s,e) => FontSize++)
				,
				new Button()
					.Text("-")
                    .Invoke(b => b.Clicked += (s,e) => FontSize--)
                ,
				new Button()
					.Text("Apply")
					.BindCommand(nameof(ApplyResourceChangeCommand))
			}
		}.Center().Padding(10);

		Size = new Size(300, 200);

		Closed += (s, e) => {
			// revert the changes in case not "applied"
			if (_fontSize != _savedOriginalValue) {
				_res[_key] = _savedOriginalValue;
			}
		};

		BindingContext = this;
    }

    [RelayCommand(CanExecute = nameof(IsApplyActive))]
    void ApplyResourceChange()
    {
        _savedOriginalValue = _fontSize;
        Close();
    }

	bool IsApplyActive() => _fontSize != _savedOriginalValue;
}

public partial class DynamicResourcePage : ContentPage
{
	static readonly int RESOURCE_DEFAULT_VALUE = 20;

	static readonly string RESOURCE_KEY = "LabelFontSize";

    static readonly string LOCALE_KEY = "UserLocale";

    #region viewmodel

    [RelayCommand]
	async Task ChangeLabelFontSize()
	{
		var popup = new ChangeFontSizeResourcePopup(Resources, RESOURCE_KEY);
		await this.ShowPopupAsync(popup);
	}

	CultureInfo _locale;

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

	AppTheme _currentTheme = Application.Current.PlatformAppTheme;

	AppTheme SelectedAppTheme 
	{
		get => _currentTheme;
		set {
			if (_currentTheme == value) return;
			_currentTheme = value;
			Application.Current.UserAppTheme = value;
            OnPropertyChanged();
		}
	}

    #endregion

    public DynamicResourcePage()
	{
        // defining a Page-wide resource
        int value = Preferences.Default.Get(RESOURCE_KEY, RESOURCE_DEFAULT_VALUE);
        Resources.Add(RESOURCE_KEY, value);

		string loc = Preferences.Get(LOCALE_KEY, CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
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