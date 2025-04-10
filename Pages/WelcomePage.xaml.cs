namespace PMU_APP.Pages;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddVehicle());
    }
}