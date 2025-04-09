using PMU_APP.Models;
using PMU_APP.Services;

namespace PMU_APP.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text?.Trim();
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            StatusLabel.Text = "Username, Email and password are required.";
            return;
        }

        if (await DatabaseService.UsernameExists(username))
        {
            StatusLabel.Text = "Username already exists.";
            return;
        }

        if (await DatabaseService.EmailExists(email))
        {
            StatusLabel.Text = "Account with this email is already registered.";
            return;
        }

        var success = await DatabaseService.RegisterUser(new User { Username = username, Email = email, Password = password });

        if (success == "Success")
        {
            await DisplayAlert("Success", "Account created!", "OK");
            await Navigation.PushAsync(new WelcomePage());
        }
        else
        {
            StatusLabel.Text = "Something went wrong.";
        }
    }
}