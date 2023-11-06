using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using dynamicresources.app.Resources.Strings;

namespace dynamicresources.app.Views;

internal partial class ChangeFontSizeResourcePopup : Popup
{
    private ResourceDictionary _res;

    private string _key = string.Empty;

    private int _savedOriginalValue;

    private int _fontSize;

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

        if (!res.ContainsKey(_key)) {
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
    private void ApplyResourceChange()
    {
        _savedOriginalValue = _fontSize;
        Close();
    }

    private bool IsApplyActive() => _fontSize != _savedOriginalValue;
}
