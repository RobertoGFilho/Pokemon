using Core.Helpers;
using Core.Navigations;
using Xamarin.Forms;

namespace Core
{
    /// <summary>
    /// Multilingual App Toolkit
    /// Windows Credentials : Generic Credential
    /// address : Multilingual/MicrosoftTranslator
    /// user : Multilingual App Toolkit
    /// Key : xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx put your azure translator service key here
    /// </summary>
    /// 
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        public App(string dbPath)
        {

            DependencyService.Register<Navigation>();
            Constants.DatabasePath = dbPath;

            UserAppTheme = OSAppTheme.Light;

            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
