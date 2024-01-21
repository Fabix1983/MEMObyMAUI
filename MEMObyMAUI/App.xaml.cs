namespace MEMObyMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new HomePage();
            Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
            Routing.RegisterRoute(nameof(NewPage), typeof(NewPage));
            Routing.RegisterRoute(nameof(InfoAppPage), typeof(InfoAppPage));
        }
    }
}
