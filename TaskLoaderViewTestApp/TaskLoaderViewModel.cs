using Sharpnado.TaskLoaderView;
using System.Diagnostics;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Xml.Serialization;
using Sharpnado.Tasks;

namespace TaskLoaderViewTestApp
{
    public interface ITaskLoaderViewModel <TData> : IDisposable
    { 
        public TaskLoaderNotifier<TData> Loader { get; }

        public ICommand CancelCommand { get; }

        public void OnAppearing();

        public void OnDisappearing();
    }




    public partial class TaskLoaderViewModel<TData> : ObservableObject, ITaskLoaderViewModel<TData>
    {
        TaskLoaderNotifier<TData> _loader;

        public TaskLoaderNotifier<TData> Loader 
        {
            get => _loader;
        }

        CancellationTokenSource _cancelationSource;

        public ICommand CancelCommand
        {
            get => CancelLoadingOrRefreshCommand;
        }

        public TaskLoaderViewModel()
        {
            _loader = new TaskLoaderNotifier<TData>();

        }

        public virtual void Dispose()
        {
            _cancelationSource.Dispose();
        }

        int _loadOrRefreshCount = 0;

        protected virtual async Task<TData> LoadDataAsync(CancellationToken token, bool isRefreshing = false)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), token);

#if DEBUG

            _loadOrRefreshCount++;
            if (isRefreshing && _loadOrRefreshCount % 2 == 0) {
                Debug.WriteLine($"GetRandomGame: {_loadOrRefreshCount}");
                throw new InvalidOperationException();
            }

            if (_loadOrRefreshCount % 2 == 0) {
                Debug.WriteLine($"GetRandomGame: {_loadOrRefreshCount}");
                throw new Exception("Some emulated loading error...");
            }

#endif

            return default(TData);
        }

        [RelayCommand]
        void CancelLoadingOrRefresh() 
        { 
            _cancelationSource?.Cancel();
        }

        [RelayCommand]
        void Load() => Loader.Load(false);

        // refresh, if last load was successfull, otherwise reload
        [RelayCommand]
        void RefreshOrReload() => Loader.Load(Loader.IsSuccessfullyCompleted);

        public virtual void OnAppearing()
        {
            _cancelationSource = new CancellationTokenSource();
            Loader.UpdateLoadingTaskSource(isRefreshing => LoadDataAsync(_cancelationSource.Token, isRefreshing));
            Loader.Load();
        }

        public virtual void OnDisappearing()
        {
            this.Dispose();
        }
    }
}
