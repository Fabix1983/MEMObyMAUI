using MEMObyMAUI.Views;

namespace MEMObyMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TabPage();
        }
    }
}
