using PMU_APP.Services;

namespace PMU_APP.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            StatusLabel.Text = "Enter both username and password.";
            return;
        }

        var user = await DatabaseService.Login(username, password);

        if (user != null)
        {
            await DisplayAlert("Welcome", $"Logged in as {user.Username}", "OK");
            await Navigation.PushAsync(new WelcomePage());

            UsernameEntry.Text = "";
            PasswordEntry.Text = "";
        }
        else
        {
            StatusLabel.Text = "Invalid credentials! Try again...";
        }
    }

    private async void OnSignupRedirect(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }
}