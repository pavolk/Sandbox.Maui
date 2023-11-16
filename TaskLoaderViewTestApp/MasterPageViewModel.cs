using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace TaskLoaderViewTestApp
{
    public partial class MasterPageViewModel : TaskLoaderViewModel<IEnumerable<string>>
    {
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();


        protected override async Task<IEnumerable<string>> LoadDataAsync(CancellationToken token, bool isRefreshing = false)
        {
            await Task.Delay(2000);

            if (!isRefreshing) {
                Items.Clear();
            }
            foreach (var it in Enumerable.Range(0, 10).Select(i => $"Item-{i}")) {
                Add(it);
            }
            return Items;
        }

        [RelayCommand]
        void Add(string item)
        {
            Items.Add(item);
        }

        [RelayCommand]
        void RemoveAll(string item)
        {
            while (Items.Remove(item)) ;
        }

    }
}
