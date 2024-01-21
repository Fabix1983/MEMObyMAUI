namespace MEMObyMAUI;

public partial class InfoAppPage : ContentPage
{
	public InfoAppPage()
	{
		InitializeComponent();
	}

    private void OnButtonHomeClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new HomePage(), true);
    }

    private void OnButtonGestisciClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new DetailsPage(), true);
    }
}