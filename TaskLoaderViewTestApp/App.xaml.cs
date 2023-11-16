using Sharpnado.TaskLoaderView;

namespace TaskLoaderViewTestApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Initializer.Initialize(true, true);

            MainPage = new AppShell();
            MainPage.HeightRequest = Preferences.Get("MainPage.Height", 800.0);
            MainPage.WidthRequest = Preferences.Get("MainPage.Height", 600.0);
            MainPage.Disappearing += (s, e) => {
                var page = s as Page;
                Preferences.Set("MainPage.Height", page.Height);
                Preferences.Set("MainPage.Width", page.Width);
            };
        }
    }
}
