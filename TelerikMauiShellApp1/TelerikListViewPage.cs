using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Input;
using System.Collections.ObjectModel;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using Telerik.Maui.Controls;
using Telerik.Maui.Controls.Compatibility.DataControls;
using Telerik.Maui.Controls.Compatibility.DataControls.ListView;
using Telerik.Maui.Controls.Compatibility.DataControls.ListView.Commands;
using Telerik.Maui.Controls.Compatibility.Common;

using CommandId = Telerik.Maui.Controls.Compatibility.DataControls.ListView.Commands.CommandId;
using ListViewCommand = Telerik.Maui.Controls.Compatibility.DataControls.ListView.Commands.ListViewCommand;

using Rows = CommunityToolkit.Maui.Markup.GridRowsColumns.Rows;

namespace TelerikMauiShellApp1;

public class HeaderFooterLabel : Label 
{
	public HeaderFooterLabel() {
		VerticalTextAlignment = TextAlignment.Center;
		HorizontalTextAlignment = TextAlignment.Start;
	}
}

static class TelerikMarkupExtensions 
{

    class ListViewCommandAdapter : ListViewCommand
    {
        ICommand _cmd;

        public ListViewCommandAdapter(CommandId id, ICommand cmd)
        {
            _cmd = cmd;
			Id = id;

        }

        public override bool CanExecute(object parameter)
        {
            return _cmd.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            _cmd.Execute(parameter);
        }
    }

    public static RadListView AddItemTapCommand(this RadListView view, ICommand cmd)
	{ 
		view.Commands.Add(new ListViewCommandAdapter(CommandId.ItemTap, cmd));
		return view;
	}

    public static RadListView AddItemHoldCommand(this RadListView view, ICommand cmd)
    {
        view.Commands.Add(new ListViewCommandAdapter(CommandId.ItemHold, cmd));
        return view;
    }

    public static RadListView AddLoadOnDemandCommand(this RadListView view, ICommand cmd)
    {
        view.Commands.Add(new ListViewCommandAdapter(CommandId.LoadOnDemand, cmd));
        return view;
    }
}

public class BaseContentPage : ContentPage
{ 
	protected BaseContentPage() 
	{
        Content = new VerticalStackLayout() {
			Children = {
				new ActivityIndicator() {
					IsRunning = true,
				},
				new Label().Text("Loading content...")
			}
		}.Center();
	}

    protected async Task<T> RunTask<T>(Func<Task<T>> asyncTask)
    {
        IsBusy = true;
        try {
            var result = await asyncTask();
            IsBusy = false;
            return result;
        } catch (Exception ex) {
            IsBusy = false;
            bool retry = await DisplayAlert("Error", ex.Message, "Retry", "Cancel");
            if (retry) {
                await DisplayAlert("", "Not implemented yet!", "Cancel");
            }
            throw;
        }
    }
}

public partial class TelerikListViewPage : BaseContentPage
{
    const int ROW_HEIGHT = 100;
    const int LOAD_DATA_DELAY_MILLIS = 2000;

    enum Row { Header, Body, Footer, TestingToolBar }

    #region view-model

    public ObservableCollection<Item> Items { get; } = new ObservableCollection<Item>();

	[RelayCommand]
	void OnItemTap(ItemTapCommandContext context)
	{
		Debug.WriteLine($"tap");
	}

	[RelayCommand]
    void OnItemHold(ItemHoldCommandContext context)
    {
        Debug.WriteLine($"hold");
    }

	int _skip = 0;

	int _take = 20;


    // TODO: add an interactive retry-loop with some countdown and navigating back...
    // TODO: add all items before triggering updated-event

    // REVISIT: This will not crash the app, but not invoke the TaskScheduler.UnobservedTaskException 
    // [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    [RelayCommand]
    async Task OnLoadData(LoadOnDemandCommandContext context)
	{
        Debug.WriteLine($"loading data...");

        try {
            var result = await RunTask(() => LoadDataAsync(_skip + _take, _take));
            foreach (var i in result) { 
                Items.Add(i);
            }
            _skip += _take;
        } catch (Exception ex) {
            Debug.WriteLine(ex.ToString());
        }
    }

    async Task OnRefreshData(object source, PullToRefreshRequestedEventArgs e)
    {
        Debug.WriteLine($"refreshing data...");

        try {
            var result = await RunTask(() => LoadDataAsync(0, _take));
            Items.Clear();
            foreach (var i in result) {
                Items.Add(i);
            }
            _skip = 0;
        } catch (Exception ex) {
            Debug.WriteLine(ex.ToString());
        }
    }

    bool _failOnNextLoadData = false;

    #endregion

    #region model

    public class Item
    {
        public string Name { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
    }

    #endregion

    #region data-service

    int _dataLoadCount = 0;

    async Task<IEnumerable<Item>> LoadDataAsync(int skip, int take)
    {
        await Task.Delay(LOAD_DATA_DELAY_MILLIS);
        if (_failOnNextLoadData || _dataLoadCount == 0) {
            _dataLoadCount++;
            throw new Exception("Loading data failed...");
        }

        return Enumerable.Range(skip, take).Select(i => new Item() {
            Name = $"Item-{i}",
            Group = $"Group-{i % 10}"
        });
    }

    #endregion

    public TelerikListViewPage()
	{
        RunTask(() => LoadDataAsync(_skip, _take))
            .ContinueWith(_ => {
                if (_.IsCompleted) {
                    Dispatcher.Dispatch(() => Build());
                } else {
                    Debug.WriteLine(_.Exception);
                }
            });
    }

	void Build()
	{
        var res = Application.Current.Resources;

        Content = new Grid() {
            RowDefinitions = Rows.Define(
                (Row.Header, 40),
                (Row.Body, Star),
                (Row.Footer, 40),
                (Row.TestingToolBar, 40)),

            Children = {
                // header
                new HeaderFooterLabel().Text("Items:")
                    .Row(Row.Header)
                ,
                // body
                new ScrollView() {
                    Content = new RadListView() {
                        IsGroupHeaderSticky = true,
                        GroupDescriptors = {
                            new ListViewDelegateGroupDescriptor() {
                                KeyExtractor = o => ((Item)o).Group
                            }
                        }
                        ,
                        IsLoadOnDemandEnabled = true,
                        LoadOnDemandMode = LoadOnDemandMode.Automatic   // not supported on Windows?
                        ,
                        IsPullToRefreshEnabled = true
                        ,
                        ItemTemplate = new DataTemplate(() => {
                            return new ListViewTemplateCell() {
                                View =
                                    new VerticalStackLayout() {
                                        Children= {
                                            new Label()
                                                .Bind(nameof(Item.Name))
                                                .Padding(5)
                                                .TextCenterVertical()
                                        }
                                    }.MinHeight(ROW_HEIGHT)
                            };
                        })
                        ,
                    }.Bind(RadListView.ItemsSourceProperty, nameof(Items))
                    .Bind(RadListView.IsLoadOnDemandActiveProperty, nameof(IsBusy))
                    .AddItemTapCommand(ItemTapCommand)
                    .AddItemHoldCommand(ItemHoldCommand)
                    .AddLoadOnDemandCommand(LoadDataCommand)
                    .Invoke(rlv => rlv.RefreshRequested += async (_,e) => {
                                await OnRefreshData(rlv,e);
                                rlv.EndRefresh();
                     })
                }.Row(Row.Body)
                ,
                // footer
                new HeaderFooterLabel().Bind(nameof(Items),
                                 convert: (IList<Item> items) => items.Count,
                                 stringFormat: "{0} items in the list.")
                    .Row(Row.Footer)
                ,
                // development toolbar
                new HorizontalStackLayout() { 
                    Spacing = 5,
                    Children = {
                        new RadCheckBox()
                            .Invoke(cb => cb.IsCheckedChanged += (s,e) => {
                                _failOnNextLoadData = e.NewValue ?? false;
                            })
                        ,
                        new Label() { 
                            VerticalTextAlignment = TextAlignment.Center,
                        }.Text("Simulate data loading error...")
                    }
                }.Row(Row.TestingToolBar)
                .Bind(View.BackgroundColorProperty, 
                        nameof(IsBusy), 
                        convert: (bool v) => v ? Colors.Red : Colors.White)
        }
        }.Padding(10);

        BindingContext = this;
        Items.CollectionChanged += (s, e) => OnPropertyChanged(nameof(Items));

    }
}