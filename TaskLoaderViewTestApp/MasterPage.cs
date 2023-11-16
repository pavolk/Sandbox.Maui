using CommunityToolkit.Maui.Markup;

namespace TaskLoaderViewTestApp
{
    public class MasterPage : TaskLoaderViewPage<IEnumerable<string>>
    {
        MasterPageViewModel vm => ViewModel as MasterPageViewModel;

        public MasterPage(MasterPageViewModel vm) : base(vm)
        {
            Title = "MasterPage";
        }

        protected override View Build()
        {
            return new CollectionView().ItemsSource(vm.Items);
        }
    }
}
